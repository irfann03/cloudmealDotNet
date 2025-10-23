from fastapi import FastAPI# type: ignore
from pydantic import BaseModel# type: ignore
from vaderSentiment.vaderSentiment import SentimentIntensityAnalyzer# type: ignore
from sentence_transformers import SentenceTransformer# type: ignore
from sklearn.metrics.pairwise import cosine_similarity # type: ignore
import numpy as np# type: ignore

# --- FastAPI app ---
app = FastAPI()

# --- Request model ---
class FeedbackRequest(BaseModel):
    comment: str

# --- Sentiment Analyzer ---
analyzer = SentimentIntensityAnalyzer()

def get_sentiment(feedback_text: str):
    score = analyzer.polarity_scores(feedback_text)['compound']
    if score >= 0.05:
        return "POSITIVE"
    else:
        return "NEGATIVE"

# --- Predefined keywords for complaint areas ---
keywords = {
    "QUALITY_ISSUE": ["quality", "bad taste", "spoiled", "stale", "raw", "undercooked", "overcooked"],
    "QUANTITY_ISSUE": ["less", "small portion", "not enough", "tiny", "insufficient", "shortage"],
    "HYGIENE_ISSUE": ["dirty", "unclean", "hygiene", "contaminated", "unsanitary", "filthy"],
    "PACKING_ISSUE": ["spill", "box", "damaged packaging", "leak", "broken", "crushed"]
}

# --- Initialize sentence embedding model ---
embedder = SentenceTransformer('all-MiniLM-L6-v2')

# --- Prepare embeddings for keywords ---
keyword_texts = []
labels = []
for label, words in keywords.items():
    for word in words:
        keyword_texts.append(word)
        labels.append(label)

keyword_embeddings = embedder.encode(keyword_texts, convert_to_tensor=True)

# --- Complaint area classification using embeddings ---
def classify_complaint(feedback_text: str):
    feedback_embedding = embedder.encode([feedback_text], convert_to_tensor=True)
    # Move both embeddings to CPU and convert to numpy
    feedback_embedding = feedback_embedding.cpu().numpy()
    keyword_embeddings_cpu = keyword_embeddings.cpu().numpy()

    similarities = cosine_similarity(feedback_embedding, keyword_embeddings_cpu)[0]
    max_idx = np.argmax(similarities)
    return labels[max_idx]

# --- API endpoint ---
@app.post("/analyze")
def analyze_feedback(feedback: FeedbackRequest):
    text = feedback.comment

    sentiment = get_sentiment(text)
    complaint_area = None

    if sentiment == "NEGATIVE":
        complaint_area = classify_complaint(text)

    return {
        "sentiment": sentiment,
        "complaint_area": complaint_area
    }
