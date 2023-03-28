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
This is used in CRQ.

For CA, VA the degree of impact on scoring is well understood using the severity (CVSS score).
For EDR, the degree of impact on scoring is well understood using the severity (Critical, High, Medium, Low, Info)

For generic signals, when the degree of impact is not well defined or not as per a standard, this field allows the
submitter to define the degree of impact.

The signal Db has the provision to auto-determine this value for well known CSPs.
A well known CSP is defined by :enum:`~PreDefinedSignalSources`

"""
class ControlType:
    detection = "detection",
    mitigation = "mitigation",
    resilience = "resilience",
    recovery = "recovery"


"""
Details about the submitter of the signal. The source needs to be an unique identifier.
This is needed for proper lifecycle management of the signal at the final consumer application.

Example: If the signal is from Azure Security Center, then the source can be "AzureSecCenter_<id>"

If the source is another custom application, then a unique id or namespace of the application to be provided here.
The application must make sure to provide the same id for all future submissions.

The Signal Db has recommended names for all well known CSP. If the submitter uses these pre-defined names,
then SAFE scoring can auto-determine the Signal.securityContext.degreeOfImpact. The well known names are listed
in {@link PreDefinedSignalSources }

The submitter must provide a Signal.securityContext.degreeOfImpact if its not any one of the {@link PreDefinedSignalSources}
"""
@dataclass
class SignalSource:
    """
    A unique name identifying the submitter.
    """
    name: str
    """
    If the submitter guarantees that it will re-submit a new state of the finding again,
    it should be mentioned here.
    
    Example: If the source is a daily scanner, then this will be 24*60
    
    Note: A signal may be marked a different {@link Signal.expiresAt} in which case, the expiresAt
    would be given preference.
    """
    nextSubmissionIntervalInMins: Optional[int] = None


"""
A geographical location. Use [ISO-3166 Alpha-2](https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes) codes
"""
@dataclass
class Location:
    countryCode: str


"""
Points to an asset management system where more details about the entity could be found.

Example: an ldap server, an ADFS server, an employee management system or an Identity management system
"""
@dataclass
class EntityManagement:
    """
    Type of the management system. Example: ldap
    """
    type: str
    """
    A set of name value pairs that can be used to infer the connection details to the system.
    """
    lookupAttributes: Dict[str, str]
    """
    Connection string, if it can be referenced by a connection string
    """
    connectionString: Optional[str] = None


"""
Represent an IP address interface
"""
@dataclass
class IpAddress:
    """
    interface name, Example: eth0, Wifi
    """
    name: str
    """
    An IP v4 in string format. It does not need to be an int32 for convenience
    """
    ipv4: Optional[str] = None
    """
    An IP v6 string.
    """
    ipv6: Optional[str] = None



"""
Attributes that describe the asset
"""
@dataclass
class EntityAttributes:
    """
    One or more IP address interfaces the asset has. A machine can have multiple interfaces as seen from the following commands
    in Windows/Unix
    ```
    ifconfig
    ```
    or
    ```
    ipconfig /all
    """
    ipAddresses: Optional[List[IpAddress]] = None
    """
    The type of machine, file or identity.
    For a machine this will be the operating system name or the service name
    Example: "Windows 10", "AWS DynamoDb"
    
    For an identity, it will be the type of identity.
    Example: user, group, role, phone, email
    
    For a file, it will be the fileType.
    Example: .pdf, .exe
    """
    type: Optional[str] = None
    """
    The criticality of the asset from the business perspective.
    """
    criticality: Optional[SeverityLevel] = None
    """
    In information security, confidentiality "is the property, that information is not made available or disclosed to
    unauthorized individuals, entities, or processes.
    This field captures the confidentiality requirement of the entity for which the signal is referring to.
    """
    confidentialityRequirement: Optional[SeverityLevel] = None
    """
    In IT security, data integrity means maintaining and assuring the accuracy and completeness of data over its entire lifecycle.
    This field captures the integrity requirement of the entity for which the signal is referring to.
    """
    integrityRequirement: Optional[SeverityLevel] = None
    """
    For any information system to serve its purpose, the information must be available when it is needed.
    This field captures the availability requirement of the entity for which the signal is referring to.
    """
    availabilityRequirement: Optional[SeverityLevel] = None
    """
    A place holder to add name value pairs as tags/labels.
    """
    tags: Optional[Dict[str, object]] = None



"""
This is a SAFE specific section which is needed for supporting SAFE managed asset on-boarding and assessment from SAFE.
This contains settings needed to remotely connect to the machine to perform a remote assessment.
"""
@dataclass
class ConnectionAttributes:
    """
    Type of connection. Example: ssh, pim, db
    """
    type: str
    """
    When connection can be represented via a connection string. Example: Db connection string
    """
    connectionString: Optional[str] = None
    """
    The login username
    """
    username: Optional[str] = None
    """
    The login password
    """
    password: Optional[str] = None
    """
    Some Unix like machines have a second password
    """
    privilegedPassword: Optional[str] = None
    """
    The remote port to connect to.
    """
    port: Optional[float] = None
    """
    SSH key, if applicable
    """
    sshKey: Optional[str] = None
    """
    SSH passphrase for the ssh key, if applicable
    """
    sshPassphrase: Optional[str] = None
    """
    Any additional properties needed to establish the connection
    """
    attributes: Optional[Dict[str, str]] = None



"""
The signal MUST contain a reference to an entity. An entity can be a machine, file or user.
"""
@dataclass
class Entity:
    """
    The type of the entity the signal applies to. See {@link EntityType}
    """
    type: EntityType
    """
    The name of the entity. Typically this should be a fully qualified name.
    
    Examples:
    - FQDN
    - IP address
    - Email id
    - Filename
    - An ARN
    - A CIDR block
    """
    name: str
    """
    Identity/Asset/File Management is a well known concept and all organization have a centralized
    asset/identity/file management software.
    
    This is true for servers and endpoints. However from ephemeral, temporary and dynamic cloud assets, they
    are sometimes not tracked by a centralized Asset Management product. Popular asset management products include,
    but not limited to:
    - CMDB
    - LDAP
    - MS Active Directory
    - Azure Active Directory
    - ServiceNow ITSM
    - IBM Maximo
    - Infor EAM
    
    The signal specification should allow submitters to add a reference to the Asset Management product.
    So, there is a need to specify
    - a type of CMDB
    - a connectionString
    - an optional list of key value that can be used to format the connection string.
    - connectionRef : Access to AssetManagement software is a sensitive information. Therefore, the signal
      submitter may supply only a reference to the asset management software as part of signal submission.
    """
    entityManagement: Union[EntityManagement, None] = field(init=False, default_factory=dict)
    """
    A reference to an asset management software may not always be feasible. A machine/identify/file has many attributes
    which are useful for business workflows. Therefore there should be an option to specify additional attributes.
    Some of these attributes hold significant business values (even though optional).
    So, the following are recommended but optional attributes of an asset
    """
    entityAttributes: Union[EntityAttributes, None] = field(init=False, default_factory=dict)
    """
    A special scenario where the signal submitter does not have any security context to share, but still submits the asset
    into the DB, with the expectation that the consumer of this signal has the ability to connect to the asset and perform
    native assessment.
    
    Example: A submitter posting a Windows or a Ubuntu or a cloud connection details, expect SAFE to on-board and retrieve
    security context.
    
    Therefore, we have a an optional ConnectionAttributes type.
    """
    connectionAttributes: Union[ConnectionAttributes, None] = field(init=False, default_factory=dict)



"""
The Common Vulnerability Scoring System (CVSS) provides a way to capture the principal characteristics of a vulnerability
and produce a numerical score reflecting its severity. This object is also used to represent CCSS 

See [CVSS specification](https://www.first.org/cvss/)
"""
@dataclass
class CVSS:
    """
    The version of CVSS used here. Example: CVSS 3.1
    """
    version: str
    """
    The CVSS score from 0-10
    """
    baseScore: float
    """
    This represents the CVSS string of all metric and its value.
    Example: AV:A/AC:H/PR:L/UI:R/S:C/C:L/I:L/A:L
    """
    vector: Optional[str] = None
    """
    Temporal score.
    """
    temporalScore: Optional[float] = None
    """
    Environmental score
    """
    environmentalScore: Optional[float] = None



"""
The severity of the signal is the most important aspect used in CRQ. Multiple vendors use their own severity system which
make it very hard to consolidate various signals from various security tools. Common variations seen among popular security
vendors include

- 0-100 score
- 0-10 score
- 0-<no high value>
- Low, Medium, High, Critical.
- 0-5

To add to complexity, a high number in some systems indicate high severity, whereas in others its the reverse.

The most popular standard widely used across the industry are

- CVSS - to communicate characteristics and severity of software vulnerabilities
- CCSS - based on CVSS, but to characterize software security configuration issues.

Non VA and CA systems or for that matter, for reporting any security incidents, usage of CVSS to characterize the incident
severity is commonly used in the industry.

The signal specification allows full characterization using CVSS/CCSS and yet allows any custom score.
"""
@dataclass
class Severity:
    """
    Indicates the type for severity. Example -cvss, ccss, custom
    """
    type: str
    """
    A numeric value that indicates the severity. Its range will vary based on the type.
    
    Example: if type=cvss, the range will be from 0-10
    """
    value: Optional[float] = None
    """
    Typically in the absence of a CVSS score, a coarse value is used here.
    This may be present even when a specific score is present.
    
    Example: A CVSS rating of 9.5 is considered to have a Severity Level = High
    """
    level: Optional[SeverityLevel] = None
    """
    The Common Vulnerability Scoring System based score.
    """
    cvss: Optional[CVSS] = None



"""
The status of the signal signifies the state of the security information. It can vary depending on the
type of signal.

Example: A Compliance signal must either be Pass, Fail or NotAssessed

Customer often mark status based on their business workflow. These are captured under workflow status.
"""
@dataclass
class Status:
    complianceStatus: Optional[ComplianceStatus] = None
    workflowStatus: Optional[WorkflowStatus] = None


"""
To accommodate a way to capture evidence of the security context.
Example: A reference to a screenshot or windows registry content that proves a missing configuration
"""
@dataclass
class Evidence:
    """
    To accommodate evidence in the form of text, comments, etc
    Example: Registry or config file values or Software versions list.
    """
    observationText: Optional[str] = None
    path: Optional[str] = None


"""
When a signal maps to certain standard, like CIS, stig, NVD, OWASP, etc, the mapping can be provided.
Example: a Signal that maps to CIS standard Section 3.1 can have
```
"name": "cisBenchMark"
"value": "9.14"
"properties": [
 {
     "cisBenchmarkVersion": "v1.0.0"
 }]
```
"""
@dataclass
class StandardMapping:
    name: str
    value: str
    properties: Optional[Dict[str, str]] = None


"""
The kill-chain-phase represents a phase in a kill chain, which describes the various phases an attacker may undertake
in order to achieve their objectives.
"""
@dataclass
class KillChainPhase:
    """
    The name of the chain. Example: lockheed-martin-cyber-kill-chain, mitre-attack
    """
    name: str
    """
    The phase. Example: reconnaissance, credential-access
    """
    phase: str


"""
Mapping to MITRE ATT&CK matrix.

Note: STIX spec does not have any easy way to
mention this mapping. This is how Signal Spec differs from STIX.
"""
@dataclass
class AttackPatternMapping:
    """
    The name of the technique. Example: "File and Directory Permissions Modification"
    """
    techniqueName: str
    """
    The technique id of TTP. Example: T1222
    """
    techniqueId: str

SignalUrl = str


"""
Attack Patterns are a type of TTP that describe ways that adversaries attempt to compromise targets. Attack Patterns are
used to help categorize attacks, generalize specific attacks to the patterns that they follow, and provide detailed
information about how attacks are performed. An example of an attack pattern is "spear phishing": a common type of attack
where an attacker sends a carefully crafted e-mail message to a party with the intent of getting them to click a link or
open an attachment to deliver malware. Attack Patterns can also be more specific; spear phishing as practiced by a
particular threat actor (e.g., they might generally say that the target won a contest) can also be an Attack Pattern.

References to externally-defined taxonomies of attacks such as [CAPEC](http://capec.mitre.org/)
"""
@dataclass
class AttackPattern:
    """
    A name, preferably using [CAPEC](http://capec.mitre.org/) terminology
    """
    name: str
    """
    The source of this reference. Example: capec,cve,microsoft365defender
    """
    sourceName: str
    """
    A human readable description
    """
    description: Union[str, SignalUrl, None] = None
    """
    The identify within the source. Example: capec-id
    """
    sourceId: Optional[str] = None
    """
    See {@link AttackPatternMapping}
    """
    mapping: Optional[AttackPatternMapping] = None


"""
A Campaign is a grouping of adversarial behaviors that describes a set of malicious activities or attacks (sometimes
called waves) that occur over a period of time against a specific set of targets. Campaigns usually have well defined
objectives and may be part of an Intrusion Set.

Campaigns are often attributed to an intrusion set and threat actors. The threat actors may reuse known infrastructure
from the intrusion set or may set up new infrastructure specific for conducting that campaign.

Campaigns can be characterized by their objectives and the incidents they cause, people or resources they target, and
the resources (infrastructure, intelligence, Malware, Tools, etc.) they use.

For example, a Campaign could be used to describe a crime syndicate's attack using a specific variant of malware and new
C2 servers against the executives of ACME Bank during the summer of 2016 in order to gain secret information about an
upcoming merger with another bank.
"""
@dataclass
class Campaign:
    """
    A name used to identify the Campaign.
    """
    name: str
    """
    Alternative names used to identify this Campaign
    """
    aliases: List[str]
    """
    The Campaign’s primary goal, objective, desired outcome, or intended effect — what the Threat Actor or Intrusion Set hopes to accomplish with this Campaign.
    """
    objective: str
    """
    The time that this Campaign was first seen.
    """
    firstSeen: str
    """
    The time that this Campaign was last seen.
    """
    lastSeen: str
    """
    A description that provides more details and context about the Campaign, potentially including its purpose and its
    key characteristics.
    """
    description: Union[str, SignalUrl, None] = None


"""
Description of the security details of this signal.
"""
@dataclass
class SecurityContextDescription:
    """
    A paragraph describing the signal where the target is the business user
    """
    business: Union[str, SignalUrl, None] = None
    """
    A paragraph describing the signal where the target is the technical user
    """
    technical: Union[str, SignalUrl, None] = None
    """
    A paragraph describing the business impact of this signal.
    """
    businessImpact: Union[str, SignalUrl, None] = None


"""
If this is a signal that has relevant remediation steps. They are to be described here
"""
@dataclass
class Remediation:
    """
    Description of the remediation.
    """
    description: Union[str, SignalUrl, None] = None
    """
    Any any references to web sites or documents
    """
    reference: Union[str, SignalUrl, None] = None
    """
    Describes the impact of remediation
    """
    impact: Union[str, SignalUrl, None] = None


@dataclass
class SecurityContext:
    """
    The most important aspect of a single is the type of security context. It can be any of the following types or something that the
    signal consumer can interpret.
    
    Some common types of signals that impact scoring, are, but not limited to:
    - Vulnerability (NVD CVEs and custom)
    - Mis-Configuration info (CIS and custom)
    - Endpoint Detection and Response (malware, virus, etc)
    - Backup Success/Failure events from Backup and Recovery Software
    - IDP/IPS/NAC/Firewall events
    - DLP events
    - Email Security events
    - SIEM events
    
    So, the type of the event is a mandatory security context.
    """ 
    type: SecurityType
    """
    The status of the signal.
    """
    status: Status
    """
    See {@link Severity}
    """
    severity: Severity
    """
    To provide additional context, subType is an optional field.
    Examples: 
    "UserAccess" can be a subType for type=uba
    "Blocked" can be a subType for type=firewall
    """
    subType: Optional[str] = None
    """
    To accommodate a way to capture evidence of the security context.
    Example: A reference to a screenshot or windows registry content that proves a missing configuration
    """
    evidence: Optional[Evidence] = None
    """
    When a signal maps to certain standard, like CIS, NVD, OWASP, etc, the mapping can be provided
    """
    standardsMapping: Optional[List[StandardMapping]] = None
    """
    A list of relevant kill chains this signal contributes to.
    """
    killChainPhases: Optional[List[KillChainPhase]] = None
    """
    A list of relevant attack patterns this signal contributes to.
    """
    attackPattern: Optional[List[AttackPattern]] = None
    """
    See {@link Campaign}
    """
    campaign: Optional[List[Campaign]] = None
    """
    To accommodate High Impact control values.
    Range from -10 to +10
    A negative impact translates to improvement in scoring
    A positive impact translates to penalty of score.
    
    0= neutral (use severity to determine impact)
    1=Low
    2=Low-Medium
    3=Medium
    3=Medium-High
    4=High
    5=High-Critical
    6=Critical
    7=Urgent
    8=BreachCertain
    9=BreachConfirmed
    """
    degreeOfImpact: Optional[int] = None
    """
    The possible effect of this signal.
    """
    effect: Optional[List[Effect]] = None
    """
    The type of control.
    """
    controlType: Optional[ControlType] = None
    """
    See {@link SecurityContextDescription}
    """
    description: Optional[SecurityContextDescription] = None
    """
    See {@link Remediation}
    """
    remediation: Optional[Remediation] = None
    """
    A place holder to add name value pairs as tags/labels.
    """
    tags: Optional[Dict[str, object]] = None



@dataclass_json
@dataclass
class Signal:
    """
    The specification of this SAFE Signal version.
    """
    version: str
    """
    A signal must have an unique id, preferably a GUID. This needs to be globally unique as it will be referenced for relationship building.
    """
    id: str
    """
    Indicates the name of the signal. It is different from the @id field as @id  represents an instance of this name.
    
    Example: Consider a CA event which you want to submit multiple times. The name could be "Password length should be 8 chars long"
    Every time you submit, you must keep this name the same else it will be treated as a different CA event.
    In every re-submission, a different @id must be used, else it will over-write a previously submitted signal.
    """
    name: str
    """
    Describe the source of the signal.
    """
    source: SignalSource
    """
    The date when the information was converted into a signal and submitted into the system
    """
    createdAt: str
    """
    A place to track refinement changes done by any user.
    
    Example:
    "Added ATT&CK mapping to the signal"
    """
    comment: Optional[str] = None
    """
    The type of signal.
    """
    type: Optional[SignalType] = SignalType.default
    """
    A paragraph that provides a human readable explanation of this signal.
    """
    description: Optional[str] = None
    """
    This is the first seen time and not when the signal object is being created as a JSON.
    """
    firstSeen: Optional[str] = None
    """
    The time when this was last seen. Typically used by connector to represent last detection time.
    """
    lastSeen: Optional[str] = None
    """
    The date when this was modified
    """
    modifiedAt: Optional[str] = None
    """
    The date when this signal is no longer relevant. Sources which guarantee that it will re-submit a new assessment
    should use this field.
    
    Example: A daily assessment submitter would have a Date = 24 hours from the time of submission.
    """
    expiresAt: Optional[str] = None
    """
    If the signal is no longer valid, mark it revoked and update the modifiedAt Date
    """
    revoked: Optional[bool] = False
    """
    A value in the range of 0-100. The confidence that the creator has in the correctness of their data.
    """
    confidence: Optional[int] = None
    """
    Some signals are relevant to only certain regions or countries. In such cases, mention the country or countries.
    Leave it blank if this signal is not region specific
    """
    location: Optional[List[Location]] = None
    """
    See {@link Entity}
    """
    entity: Optional[Entity] = None
    """
    Every signal MUST contain at least one interesting security information that contributes to SAFE score. This information can
    be either a positive or a negative contributor to the score.
    
    Example:
    - An antivirus agent installed on en Endpoint is a positive security context
    - A malware detection or a CA control failing is a negative security context.
    
    STIX, CAPEC, ATT&CK
    -------------------
    STIX’s Domain Objects (SDOs) include Attack Pattern, Campaign, Course of Action, Grouping, Identity, Indicator, Infrastructure,
    Intrusion Set, Location, Malware, Malware Analysis, Note, Observed Data, Opinion, Report, Threat Actor, Tool, and Vulnerability.
    
    STIX Cyber-observable Objects (SCOs) document the facts concerning what happened on a network or host.By associating SCOs with
    STIX Domain Objects (SDOs), it is possible to convey a higher-level understanding of the threat landscape, and to potentially
    provide insight as to the who and the why particular intelligence may be relevant to an organization.
    
    A single SAFE Signal contains an asset and its securityContext which maps to STIX's SDO and SCOs respectively. CAPEC provides a
    comprehensive dictionary of known patterns of attack employed by adversaries to exploit known weaknesses in cyber-enabled capabilities.
    It can be used by analysts, developers, testers, and educators to advance community understanding and enhance defenses. CAPEC is
    focused on application security and describes the common attributes and techniques employed by adversaries to exploit known
    weaknesses in cyber-enabled capabilities.
    (e.g., SQL Injection, XSS, Session Fixation, Click-jacking).
    
    ATT&CK is a knowledge base of cyber adversary behavior and taxonomy for adversarial actions across their lifecycle. ATT&CK has
    two parts- Enterprise and Mobile. ATT&CK is focused on network defense and describes the operational phases in an adversary’s
    lifecycle, pre and post-exploit (e.g., Persistence, Lateral Movement, Exfiltration), and details the specific tactics, techniques,
    and procedures (TTPs) that advanced persistent threats (APT) use to execute their objectives while targeting, compromising, and
    operating inside a network.
    
    #### How are they (ATT&CK, CAPEC, STIX) related to SAFE Signal?
    Many attack patterns enumerated by CAPEC are employed by adversaries through specific techniques described by ATT&CK. STIX is a
    specification that allows describing them.SAFE Signal is motivated by STIX.
    """
    securityContext: Optional[SecurityContext] = None
    """
    If the signal contains more than one security information,the information can be sent as array of security information.
    """
    securityContexts: Optional[List[SecurityContext]] = None
    """
    Information which do not describe the technical nature of the signal, but necessary for understanding the impact on business
    are provided in this section. This information may vary from organization to organization.
    
    Examples:
    - Cost of remediation of a vulnerability signal
    - Potential impact on cyber-insurance due to this signal.
    
    This is to be provided in the form of key-value pairs. The signal specification currently does not have any standard for naming them
    """
    businessContext: Optional[Dict[str, List[str]]] = None
    """
    A place holder to add name value pairs as tags/labels.    
    """
    tags: Optional[Dict[str, object]] = None
