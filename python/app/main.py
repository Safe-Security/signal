from utils.signals import submitSampleSignals
from config import Config

def main():
    # Initializing config data
    Config()
    submitSampleSignals()
    
if __name__ == "__main__": 
    main()
