import logging
import json
import azure.functions as func
import os
from azure.eventhub import EventHubProducerClient, EventData

def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')
        
    connection_str = os.environ.get("EventHubConnectionString")
    eventhub_name = 'newactivityhub'
    
    client  = EventHubProducerClient.from_connection_string(connection_str, eventhub_name=eventhub_name)
    
    event_data_batch = client.create_batch()
    event_data_batch.add(EventData('Message inside EventBatchData'))
    
    client.send_batch(event_data_batch)
    

"""
    hub_challenge  = req.params.get('hub.challenge')

    response = {"hub.challenge" : hub_challenge}
    
    return func.HttpResponse(
        json.dumps(response),
        mimetype="application/json",
    )
"""
