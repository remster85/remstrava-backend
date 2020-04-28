import logging
import json
import azure.functions as func

def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')
    
    hub_challenge  = req.params.get('hub.challenge')

    response = {"hub.challenge" : hub_challenge}
    
    return func.HttpResponse(
        json.dumps(response),
        mimetype="application/json",
    )
