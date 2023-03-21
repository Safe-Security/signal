# Overview
The document contains steps to develop a SAFE connector. 

# SAFE Connector
SAFE has the ability to ingest security signals from various sources to perform its analysis. These signals can be CA (Configuration Assessment), VA (Vulnerability Assessment), EDR (Endpoint Detection and Response), etc coming from various tools.

Connectors for popular products and services are already available out of the box for SAFE's customer. See the [Integration Guide](https://docs.safe.security/safe3/docs/integration-guide) for the list.

This document allows developers to build a custom connector with any tool which generates signals of interest.

# Quick Start Guide
### Prerequisites
- An instance of SAFE and its API credentials.

We will post a simple CA signal to SAFE. On posting this signal, you will be able to see the Breach Likelihood and Financial Risk Exposure in SAFE UI. For all practical purposes, hundreds of signals would be needed to generate a useful risk scenario, but a single signal can be used to demonstrate the concept of building a SAFE connector.

### NodeJS sample connector

### C# .NET sample connector

### Python sample connector



# Use cases
The following are some of the use cases for building a custom connector.
- As a SAFE customer, I have a product which generates security events. SAFE does not natively support this tool and I want to submit signals to my SAFE instance.

- As a community contributor, I want to build an integration so that all SAFE customers can deploy my connector.

- As a SAFE Engineering team member, I want to build a new connector. 

# Hosting the connector
The connector workload can be hosted anywhere as long as it can connect to SAFE REST APIs via HTTPS.

# Connector Architecture
The diagram below shows the relation between a connector and SAFE.

# Connector to SAFE Authentication
Coming soon ...

# FAQ
Coming soon...