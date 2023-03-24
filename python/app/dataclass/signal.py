"""
Signal - 

Any kind of information about an asset that would be interesting from a security scoring point of view.
This specification is motivated and uses references from STIX specification. You will notice several attribute name
and description matching the [STIX 2.1](https://docs.oasis-open.org/cti/stix/v2.1/os/stix-v2.1-os.html) specification.

[STIX](https://oasis-open.github.io/cti-documentation/stix/intro) is designed to improve many different capabilities, such as collaborative threat analysis, automated threat exchange,
automated detection and response, and more.

STIX has a far wider scope. While STIX is a well defined standard, its adoption within Security Products has been limited
as most Security products deals with a small aspect of Cyber Security. Most security products therefore provide STIX
interfaces to generate or consume STIX objects.

Safe Signal focuses on understanding Signals which contribute to CRQ. This project will support importing and exporting to STIX format.

Need for Signal spec when STIX is already present - 
STIX is a complex system, in the sense that it has a long learning curve. Its main goal is cross-collaboration between organization
to exchange Cyber Threat Intelligence. STIX is a connected graph of nodes and edges.  See STIX Spec document here. The SAFE Signal
specification is motivated by
 - Those parts of STIX that can contribute to CQR
 - Aligned to existing SAFE architecture, which focuses on a layered scoring system for CISOs.

SAFE Signal spec as the step towards adopting all aspects of CTI (Cyber Threat Intelligence).

Background and Requirements
---------------------------
SAFE (as of June, 2022) understands CA, VA and EDR signals only. These are not all the signals that an organization has. Therefore
there is a need to ingest all interesting signals for a holistic scoring and risk calculation.

String value convention for fields
----------------------------------
There are several attributes of type
```
string | SignalUrl
```
SignalUrl is a reference to another signal where the details can be found. This allows non-repetition of some long strings.
Example:
The Signal.securityContext.remediation.description can be a bug paragraph. A submitter may be submitting the same signal for the
asset to the Signal Db on a regular or daily basis. There is no need to submit large para to be submitted in every signal.
A signal attribute can refer to another signal via the signal Url, via the signal.id field. So a submitter of a signal may references
it via SignalUrl as
```
remediation = "signalurl://<id of the original signal>"
```
The above will refer to the remediation text of the referred signal.
"""

from dataclasses import dataclass, field
from typing import Dict, List, Optional, Union

from dataclasses_json import dataclass_json

"""
The signal specification allows submitting a complete signal (with entity and security context) which
is the default use case.

For the use case of daily assessments, there are information which are duplicate. Signal specification
allows the flexibility to the signal submitter to choose to post asset details alone which can later be
used as a reference in full signal submission.

Example: A submitter would need to post a few 100 signals
for an asset on a daily basis. It is not needed to submit full details about the asset each time. This
approach is motivated by Azure and GCP security center's approach where they have the concept of resources
and findings as separate objects.

A signal of type=entityOnly is a resource with an unique id. A signal
of type=securityContext is a Finding with a unique id and referencing an entity.
"""
class SignalType:
    default = "default",
    entityOnly = "entityOnly",
    securityContextOnly = "securityContextOnly"

"""
A signal should always refer to an object on which the security threat applies to.

It can be a machine, machines in the form of hostname(s), IP addresses (IPv4 and IPv6)
or a network CIDR or even a set of diverse resources like a cloud account (an ARN as an example)

A file which is generally the object when reporting a malware.

A user which is not necessarily a human user. This is typically characterized by an email id or a system user id.
"""
class EntityType:
    machine = "machine",
    file = "file",
    identity = "identity",
    organization = "organization"


"""
A coarse severity level for signal.
"""
class SeverityLevel:
    critical = "critical"
    high = "high"
    medium = "medium"
    low = "low"
    info = "info"


"""
These are the list of different types of Cyber Security types of signals supported by this specification.
The enum type is specifically string and not an auto-increment number. This is because dealing with numbers
makes data-lake queries and reports hard to understand. Numeric values are useful for machines and highly
responsive applications.
"""
class SecurityType:
    finding = "finding",
    ca = "ca",
    va = "va",
    edr = "edr",
    log = "log",
    backup = "backup",
    network = "network",
    dlp = "dlp",
    email = "email",
    uba= "uba",
    waf="waf",
    others = "others"


"""
Status applicable to compliance signals.
"""
class ComplianceStatus:
    PASS = "pass",
    FAIL = "fail",
    UNKNOWN = "unknown"


"""
Status applicable to capture workflow state from the source of signal.
"""
class WorkflowStatus:
    new = "new",
    resolved = "resolved"


"""
Possible effect of the signal. The values are motivated by AWS ASFF spec.
"""
class Effect:
    dataExposure = "dataExposure",
    dataExfiltration = "dataExfiltration",
    dataDestruction = "dataDestruction",
    denialOfService = "denialOfService",
    resourceConsumption = "resourceConsumption"


class PreDefinedSignalSources:
    qualysCa = "com.qualys.ca",
    taniumComply = "com.tanium.comply"

"""
A signal can have different types of impact on the security posture.
 * This is used in CRQ.
 *
 * For CA, VA the degree of impact on scoring is well understood using the severity (CVSS score).
 * For EDR, the degree of impact on scoring is well understood using the severity (Critical, High, Medium, Low, Info)
 *
 * For generic signals, when the degree of impact is not well defined or not as per a standard, this field allows the
 * submitter to define the degree of impact.
 *
 * The signal Db has the provision to auto-determine this value for well known CSPs.
 * A well known CSP is defined by :enum:`~PreDefinedSignalSources`

"""
class ControlType:
    detection = "detection",
    mitigation = "mitigation",
    resilience = "resilience",
    recovery = "recovery"

@dataclass
class SignalSource:
    name: str
    nextSubmissionIntervalInMins: Optional[int] = None

@dataclass
class Location:
    countryCode: str

@dataclass
class EntityManagement:
    type: str
    lookupAttributes: Dict[str, str]
    connectionString: Optional[str] = None

@dataclass
class IpAddress:
    name: str
    ipv4: Optional[str] = None
    ipv6: Optional[str] = None

@dataclass
class EntityAttributes:
    ipAddresses: Optional[List[IpAddress]] = None
    type: Optional[str] = None
    criticality: Optional[SeverityLevel] = None
    confidentialityRequirement: Optional[SeverityLevel] = None
    integrityRequirement: Optional[SeverityLevel] = None
    availabilityRequirement: Optional[SeverityLevel] = None
    tags: Optional[Dict[str, object]] = None

@dataclass
class ConnectionAttributes:
    type: str
    connectionString: Optional[str] = None
    username: Optional[str] = None
    password: Optional[str] = None
    privilegedPassword: Optional[str] = None
    port: Optional[float] = None
    sshKey: Optional[str] = None
    sshPassphrase: Optional[str] = None
    attributes: Optional[Dict[str, str]] = None

@dataclass
class Entity:
    type: EntityType
    name: str
    entityManagement: Union[EntityManagement, None] = field(init=False, default_factory=dict)
    entityAttributes: Union[EntityAttributes, None] = field(init=False, default_factory=dict)
    connectionAttributes: Union[ConnectionAttributes, None] = field(init=False, default_factory=dict)

@dataclass
class CVSS:
    version: str
    baseScore: float
    vector: Optional[str] = None
    temporalScore: Optional[float] = None
    environmentalScore: Optional[float] = None

@dataclass
class Severity:
    type: str
    value: Optional[float] = None
    level: Optional[SeverityLevel] = None
    cvss: Optional[CVSS] = None

@dataclass
class Status:
    complianceStatus: Optional[ComplianceStatus] = None
    workflowStatus: Optional[WorkflowStatus] = None

@dataclass
class Evidence:
    observationText: Optional[str] = None
    path: Optional[str] = None

@dataclass
class StandardMapping:
    name: str
    value: str
    properties: Optional[Dict[str, str]] = None

@dataclass
class KillChainPhase:
    name: str
    phase: str

@dataclass
class AttackPatternMapping:
    techniqueName: str
    techniqueId: str

SignalUrl = str

@dataclass
class AttackPattern:
    name: str
    sourceName: str
    description: Union[str, SignalUrl, None] = None
    sourceId: Optional[str] = None
    mapping: Optional[AttackPatternMapping] = None

@dataclass
class Campaign:
    name: str
    aliases: List[str]
    objective: str
    firstSeen: str
    lastSeen: str
    description: Union[str, SignalUrl, None] = None

@dataclass
class SecurityContextDescription:
    business: Union[str, SignalUrl, None] = None
    technical: Union[str, SignalUrl, None] = None
    businessImpact: Union[str, SignalUrl, None] = None

@dataclass
class Remediation:
    description: Union[str, SignalUrl, None] = None
    reference: Union[str, SignalUrl, None] = None
    impact: Union[str, SignalUrl, None] = None

@dataclass
class SecurityContext: 
    type: SecurityType
    status: Status
    severity: Severity
    subType: Optional[str] = None
    evidence: Optional[Evidence] = None
    standardsMapping: Optional[List[StandardMapping]] = None
    killChainPhases: Optional[List[KillChainPhase]] = None
    attackPattern: Optional[List[AttackPattern]] = None
    campaign: Optional[List[Campaign]] = None
    degreeOfImpact: Optional[int] = None
    effect: Optional[List[Effect]] = None
    controlType: Optional[ControlType] = None
    description: Optional[SecurityContextDescription] = None
    remediation: Optional[Remediation] = None
    tags: Optional[Dict[str, object]] = None

@dataclass_json
@dataclass
class Signal:
    version: str

    id: str

    name: str

    source: SignalSource

    createdAt: str

    comment: Optional[str] = None

    firstSeen: Optional[str] = None

    type: Optional[SignalType] = SignalType.default

    description: Optional[str] = None

    lastSeen: Optional[str] = None

    modifiedAt: Optional[str] = None

    expiresAt: Optional[str] = None

    revoked: Optional[bool] = False

    tags: Optional[Dict[str, object]] = None

    confidence: Optional[int] = None

    location: Optional[List[Location]] = None

    entity: Optional[Entity] = None

    securityContext: Optional[SecurityContext] = None

    securityContexts: Optional[List[SecurityContext]] = None

    businessContext: Optional[Dict[str, List[str]]] = None
