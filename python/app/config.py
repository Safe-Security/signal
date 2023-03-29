from configparser import ConfigParser

class Config:
    base_url = None
    api_username = None
    api_password = None

    def __init__(self):
        config_parser = ConfigParser()
        config_parser.read('config.ini')
        server_config = config_parser['server']
        Config.base_url = server_config['SafeUrl']
        Config.api_username = server_config['ApiUsername']
        Config.api_password = server_config['ApiPassword']
        if not Config.base_url or not Config.api_username or not Config.api_password:
            print('Server details missing. Please add server details in the file "config.ini". Exiting.')
            exit(1)
