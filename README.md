This project defines the [SAFE](https://safe.security) Security platform's Signal Specification and guide to build a connector for SAFE platform. 

SAFE (Security Assessment Framework for Enterprise) is an enterprise-class, unified, and real-time Cyber Risk Quantification and Management (CRQM) platform. To learn more about SAFE see [https://docs.safe.security](https://docs.safe.security)

# Who is the intended audience?
The intended audience for this project are developers, solution-architects who
- would like to understand the concept of Signal used in SAFE.
- would like to build a custom connector to ingest more signals into their SAFE platform.
- a community developer who would like to further enhance this project.

# What is a Signal?
Signal, is the smallest unit of information that contains interesting information about an enterprise customer, the knowledge of which, allows SAFE to quantify risk for the customer.

A signal contains two fundamental properties:

A reference to an entity related to the customer. This can be a machine, identity, or file. [An identity operates on a file using a machine - this covers the entire software usage in the world]. 

A security-context about the entity. Every signal MUST contain at least one interesting security information that contributes to risk. This information can be either a positive or a negative contributor to risk- score.

Examples:
- An antivirus agent installed on en Endpoint is a positive security context
- A malware detection or a CA control failing is a negative security context.


# Signal Specification
See [README.md](nodejs/README.md) on steps for generating the specification docs. You will need a nodejs environment to generate the docs.

# Building a connector
See the [connector developer guide](/developer-guide.md) documentation. 

# Signal examples
See the folder [examples/samples](/examples/samples/)

# Sample connectors (programs)
Read the connector developer guide or jump straight to sample programs in various programming languages in [examples/connectors](/examples/connectors/)
