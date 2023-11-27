/* eslint-disable no-secrets/no-secrets */
/* eslint-disable no-shadow */

/**
 * Signal
 * ======
 * Any kind of information about an asset that would be interesting from a security scoring point of view.
 * This specification is motivated and uses references from STIX specification. You will notice several attribute name
 * and description matching the [STIX 2.1](https://docs.oasis-open.org/cti/stix/v2.1/os/stix-v2.1-os.html) specification.
 *
 * [STIX](https://oasis-open.github.io/cti-documentation/stix/intro) is designed to improve many different capabilities, such as collaborative threat analysis, automated threat exchange,
 * automated detection and response, and more.
 *
 * STIX has a far wider scope. While STIX is a well defined standard, its adoption within Security Products has been limited
 * as most Security products deals with a small aspect of Cyber Security. Most security products therefore provide STIX
 * interfaces to generate or consume STIX objects.
 *
 * Safe Signal focuses on understanding Signals which contribute to CRQ. This project will support importing and exporting to STIX format.
 *
 * Need for Signal spec when STIX is already present
 * -------------------------------------------------
 * STIX is a complex system, in the sense that it has a long learning curve. Its main goal is cross-collaboration between organization
 * to exchange Cyber Threat Intelligence. STIX is a connected graph of nodes and edges.  See STIX Spec document here. The SAFE Signal
 * specification is motivated by
 * - Those parts of STIX that can contribute to CQR
 * - Aligned to existing SAFE architecture, which focuses on a layered scoring system for CISOs.
 *
 * SAFE Signal spec as the step towards adopting all aspects of CTI (Cyber Threat Intelligence).
 *
 * Background and Requirements
 * ---------------------------
 * SAFE (as of June, 2022) understands CA, VA and EDR signals only. These are not all the signals that an organization has. Therefore
 * there is a need to ingest all interesting signals for a holistic scoring and risk calculation.
 *
 * String value convention for fields
 * ----------------------------------
 * There are several attributes of type
 * ```
 * string | SignalUrl
 * ```
 * SignalUrl is a reference to another signal where the details can be found. This allows non-repetition of some long strings.
 * Example:
 * The Signal.securityContext.remediation.description can be a bug paragraph. A submitter may be submitting the same signal for the
 * asset to the Signal Db on a regular or daily basis. There is no need to submit large para to be submitted in every signal.
 * A signal attribute can refer to another signal via the signal Url, via the signal.id field. So a submitter of a signal may references
 * it via SignalUrl as
 * ```
 * remediation = "signalurl://<id of the original signal>"
 * ```
 * The above will refer to the remediation text of the referred signal.
 *
 */
export interface Signal {
    /**
     * The specification of this SAFE Signal version.
     */
    version: string;

    /**
     * A signal must have an unique id, preferably a GUID. This needs to be globally unique as it will be referenced for relationship building.
     */
    id: string;

    /**
     * Indicates the name of the signal. It is different from the @id field as @id  represents an instance of this name.
     *
     * Example: Consider a CA event which you want to submit multiple times. The name could be "Password length should be 8 chars long"
     * Every time you submit, you must keep this name the same else it will be treated as a different CA event.
     * In every re-submission, a different @id must be used, else it will over-write a previously submitted signal.
     */
    name: string;

    /**
     * Describe the source of the signal.
     */
    source: SignalSource;

    /**
     * The type of signal.
     */
    type?: SignalType;

    /**
     * A paragraph that provides a human readable explanation of this signal.
     */
    description?: string;

    /**
     * The date when the information was converted into a signal and submitted into the system
     */
    createdAt: Date;

    /**
     * This is the first seen time and not when the signal object is being created as a JSON.
     */
    firstSeen?: Date;

    /**
     * The time when this was last seen. Typically used by connector to represent last detection time.
     */
    lastSeen?: Date;

    /**
     * The date when this was modified
     */
    modifiedAt?: Date;

    /**
     * The date when this signal is no longer relevant. Sources which guarantee that it will re-submit a new assessment
     * should use this field.
     *
     * Example: A daily assessment submitter would have a Date = 24 hours from the time of submission.
     */
    expiresAt?: Date;

    /**
     * If the signal is no longer valid, mark it revoked and update the modifiedAt Date
     */
    revoked?: boolean;

    /**
     * A place holder to add name value pairs as tags/labels.
     */
    tags?: { [key: string]: string[] };

    /**
     * A value in the range of 0-100. The confidence that the creator has in the correctness of their data.
     */
    confidence?: number;

    /**
     * Some signals are relevant to only certain regions or countries. In such cases, mention the country or countries.
     * Leave it blank if this signal is not region specific
     */
    location?: Location[];

    /**
     * A place to track refinement changes done by any user.
     *
     * Example:
     * "Added ATT&CK mapping to the signal"
     */
    comment?: string;

    /**
     * The signal MUST contain a reference to an entity. An entity can be a machine, file or user.
     */
    entity?: {
        /**
         * The type of the entity the signal applies to. See {@link EntityType}
         */
        type: EntityType;

        /**
         * The name of the entity. Typically this should be a fully qualified name.
         *
         * Examples:
         * - FQDN
         * - IP address
         * - Email id
         * - Filename
         * - An ARN
         * - A CIDR block
         */
        name: string;

        /**
         * Identity/Asset/File Management is a well known concept and all organization have a centralized
         * asset/identity/file management software.
         *
         * This is true for servers and endpoints. However from ephemeral, temporary and dynamic cloud assets, they
         * are sometimes not tracked by a centralized Asset Management product. Popular asset management products include,
         * but not limited to:
         * - CMDB
         * - LDAP
         * - MS Active Directory
         * - Azure Active Directory
         * - ServiceNow ITSM
         * - IBM Maximo
         * - Infor EAM
         *
         * The signal specification should allow submitters to add a reference to the Asset Management product.
         * So, there is a need to specify
         * - a type of CMDB
         * - a connectionString
         * - an optional list of key value that can be used to format the connection string.
         * - connectionRef : Access to AssetManagement software is a sensitive information. Therefore, the signal
         *   submitter may supply only a reference to the asset management software as part of signal submission.
         */
        entityManagement?: EntityManagement;

        /**
         * A reference to an asset management software may not always be feasible. A machine/identify/file has many attributes
         * which are useful for business workflows. Therefore there should be an option to specify additional attributes.
         * Some of these attributes hold significant business values (even though optional).
         * So, the following are recommended but optional attributes of an asset
         */
        entityAttributes?: EntityAttributes;

        /**
         * A special scenario where the signal submitter does not have any security context to share, but still submits the asset
         * into the DB, with the expectation that the consumer of this signal has the ability to connect to the asset and perform
         * native assessment.
         *
         * Example: A submitter posting a Windows or a Ubuntu or a cloud connection details, expect SAFE to on-board and retrieve
         * security context.
         *
         * Therefore, we have a an optional ConnectionAttributes type.
         */
        connectionAttributes?: ConnectionAttributes;
    };

    /**
     * Every signal MUST contain at least one interesting security information that contributes to SAFE score. This information can
     * be either a positive or a negative contributor to the score.
     *
     * Example:
     * - An antivirus agent installed on en Endpoint is a positive security context
     * - A malware detection or a CA control failing is a negative security context.
     *
     * STIX, CAPEC, ATT&CK
     * -------------------
     * STIX’s Domain Objects (SDOs) include Attack Pattern, Campaign, Course of Action, Grouping, Identity, Indicator, Infrastructure,
     * Intrusion Set, Location, Malware, Malware Analysis, Note, Observed Data, Opinion, Report, Threat Actor, Tool, and Vulnerability.
     *
     * STIX Cyber-observable Objects (SCOs) document the facts concerning what happened on a network or host.By associating SCOs with
     * STIX Domain Objects (SDOs), it is possible to convey a higher-level understanding of the threat landscape, and to potentially
     * provide insight as to the who and the why particular intelligence may be relevant to an organization.
     *
     * A single SAFE Signal contains an asset and its securityContext which maps to STIX's SDO and SCOs respectively. CAPEC provides a
     * comprehensive dictionary of known patterns of attack employed by adversaries to exploit known weaknesses in cyber-enabled capabilities.
     * It can be used by analysts, developers, testers, and educators to advance community understanding and enhance defenses. CAPEC is
     * focused on application security and describes the common attributes and techniques employed by adversaries to exploit known
     * weaknesses in cyber-enabled capabilities.
     * (e.g., SQL Injection, XSS, Session Fixation, Click-jacking).
     *
     * ATT&CK is a knowledge base of cyber adversary behavior and taxonomy for adversarial actions across their lifecycle. ATT&CK has
     * two parts- Enterprise and Mobile. ATT&CK is focused on network defense and describes the operational phases in an adversary’s
     * lifecycle, pre and post-exploit (e.g., Persistence, Lateral Movement, Exfiltration), and details the specific tactics, techniques,
     * and procedures (TTPs) that advanced persistent threats (APT) use to execute their objectives while targeting, compromising, and
     * operating inside a network.
     *
     * #### How are they (ATT&CK, CAPEC, STIX) related to SAFE Signal?
     * Many attack patterns enumerated by CAPEC are employed by adversaries through specific techniques described by ATT&CK. STIX is a
     * specification that allows describing them.SAFE Signal is motivated by STIX.
     *
     *
     */
    securityContext?: SecurityContext;

    //If the signal contains more than one security information,the information can be sent as array of security information.

    securityContexts?:SecurityContext[];

    /**
     * Information which do not describe the technical nature of the signal, but necessary for understanding the impact on business
     * are provided in this section. This information may vary from organization to organization.
     *
     * Examples:
     * - Cost of remediation of a vulnerability signal
     * - Potential impact on cyber-insurance due to this signal.
     *
     *  This is to be provided in the form of key-value pairs. The signal specification currently does not have any standard for naming them
     */
    businessContext?: { [key: string]: string[] };
}

export enum PreDefinedSignalSources {
    qualysCa = "com.qualys.ca",
    taniumComply = "com.tanium.comply"
}

/**
 * Details about the submitter of the signal. The source needs to be an unique identifier.
 * This is needed for proper lifecycle management of the signal at the final consumer application.
 *
 * Example: If the signal is from Azure Security Center, then the source can be "AzureSecCenter_<id>"
 *
 * If the source is another custom application, then a unique id or namespace of the application to be provided here.
 * The application must make sure to provide the same id for all future submissions.
 *
 * The Signal Db has recommended names for all well known CSP. If the submitter uses these pre-defined names,
 * then SAFE scoring can auto-determine the Signal.securityContext.degreeOfImpact. The well known names are listed
 * in {@link PreDefinedSignalSources }
 *
 * The submitter must provide a Signal.securityContext.degreeOfImpact if its not any one of the {@link PreDefinedSignalSources}
 */
export interface SignalSource {
    /**
     * A unique name identifying the submitter.
     */
    name: string;

    /**
     * If the submitter guarantees that it will re-submit a new state of the finding again,
     * it should be mentioned here.
     *
     * Example: If the source is a daily scanner, then this will be 24*60
     *
     * Note: A signal may be marked a different {@link Signal.expiresAt} in which case, the expiresAt
     * would be given preference.
     *
     */
    nextSubmissionIntervalInMins?: number;
}

/**
 * A signal can have different types of impact on the security posture.
 * This is used in CRQ.
 *
 * For CA, VA the degree of impact on scoring is well understood using the severity (CVSS score).
 * For EDR, the degree of impact on scoring is well understood using the severity (Critical, High, Medium, Low, Info)
 *
 * For generic signals, when the degree of impact is not well defined or not as per a standard, this field allows the
 * submitter to define the degree of impact.
 *
 * The signal Db has the provision to auto-determine this value for well known CSPs.
 * A well known CSP is defined by {@link PreDefinedSignalSources}
 *
 */

export enum ControlType {
    /**
     * Indicates that the signal represents a detection control.
     */
    detection = "detection",

    /**
     * Indicates that the signal represents a mitigation control.
     */
    mitigation = "mitigation",

    /**
     * Indicates that the signal represents a resilience control.
     */
    resilience = "resilience",

    /**
     * Indicates that the signal represents a recovery Control.
     */
    recovery = "recovery"
}

/**
 * When a signal maps to certain standard, like CIS, stig, NVD, OWASP, etc, the mapping can be provided.
 * Example: a Signal that maps to CIS standard Section 3.1 can have
 * ```
 * "name": "cisBenchMark"
 * "value": "9.14"
 * "properties": [
 *  {
 *      "cisBenchmarkVersion": "v1.0.0"
 *  }]
 * ```
 */
export interface StandardMapping {
    name: string;
    value: string;
    properties?: { [key: string]: string };
}

// eslint-disable-next-line no-secrets/no-secrets
/**
 * A geographical location. Use [ISO-3166 Alpha-2](https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes) codes
 */
export interface Location {
    countryCode: string;
}

/**
 * A Campaign is a grouping of adversarial behaviors that describes a set of malicious activities or attacks (sometimes
 * called waves) that occur over a period of time against a specific set of targets. Campaigns usually have well defined
 * objectives and may be part of an Intrusion Set.
 *
 * Campaigns are often attributed to an intrusion set and threat actors. The threat actors may reuse known infrastructure
 * from the intrusion set or may set up new infrastructure specific for conducting that campaign.
 *
 * Campaigns can be characterized by their objectives and the incidents they cause, people or resources they target, and
 * the resources (infrastructure, intelligence, Malware, Tools, etc.) they use.
 *
 * For example, a Campaign could be used to describe a crime syndicate's attack using a specific variant of malware and new
 * C2 servers against the executives of ACME Bank during the summer of 2016 in order to gain secret information about an
 * upcoming merger with another bank.
 */
export interface Campaign {
    /**
     * A name used to identify the Campaign.
     */
    name: string;

    /**
     * Alternative names used to identify this Campaign
     */

    aliases: string[];
    /**
     * The Campaign’s primary goal, objective, desired outcome, or intended effect — what the Threat Actor or Intrusion Set hopes to accomplish with this Campaign.
     */
    objective: string;

    /**
     * A description that provides more details and context about the Campaign, potentially including its purpose and its
     * key characteristics.
     */
    description?: string | SignalUrl;

    /**
     * The time that this Campaign was first seen.
     */
    firstSeen: Date;

    /**
     * The time that this Campaign was last seen.
     */
    lastSeen: Date;
}

/**
 * Attack Patterns are a type of TTP that describe ways that adversaries attempt to compromise targets. Attack Patterns are
 * used to help categorize attacks, generalize specific attacks to the patterns that they follow, and provide detailed
 * information about how attacks are performed. An example of an attack pattern is "spear phishing": a common type of attack
 * where an attacker sends a carefully crafted e-mail message to a party with the intent of getting them to click a link or
 * open an attachment to deliver malware. Attack Patterns can also be more specific; spear phishing as practiced by a
 * particular threat actor (e.g., they might generally say that the target won a contest) can also be an Attack Pattern.
 *
 *  References to externally-defined taxonomies of attacks such as [CAPEC](http://capec.mitre.org/)
 */
export interface AttackPattern {
    /**
     * A name, preferably using [CAPEC](http://capec.mitre.org/) terminology
     */
    name: string;

    /**
     * A human readable description
     */
    description?: string | SignalUrl;

    /**
     * The source of this reference. Example: capec,cve,microsoft365defender
     */
    sourceName: string;

    /**
     * The identify within the source. Example: capec-id
     */
    sourceId?: string;

    /**
     * Mapping to MITRE ATT&CK matrix.
     *
     * Note: STIX spec does not have any easy way to
     * mention this mapping. This is how Signal Spec differs from STIX.
     */
    mapping?: {
        /**
         * The name of the technique. Example: "File and Directory Permissions Modification"
         */
        techniqueName: string;

        /**
         * The technique id of TTP. Example: T1222
         */
        techniqueId: string;
    };
}

/**
 * The kill-chain-phase represents a phase in a kill chain, which describes the various phases an attacker may undertake
 * in order to achieve their objectives.
 */
export interface KillChainPhase {
    /**
     * The name of the chain. Example: lockheed-martin-cyber-kill-chain, mitre-attack
     */
    name: string;

    /**
     * The phase. Example: reconnaissance, credential-access
     */
    phase: string;
}

/**
 * The Common Vulnerability Scoring System (CVSS) provides a way to capture the principal characteristics of a vulnerability
 * and produce a numerical score reflecting its severity. This object is also used to represent CCSS 
 *
 * See [CVSS specification](https://www.first.org/cvss/)
 */
export interface CVSS {
    /**
     * The version of CVSS used here. Example: CVSS 3.1
     */
    version: string;

    /**
     * This represents the CVSS string of all metric and its value.
     * Example: AV:A/AC:H/PR:L/UI:R/S:C/C:L/I:L/A:L
     */
    vector?: string;
    
    /**
     * The CVSS score from 0-10
     */
    baseScore: number;

    /**
     * Temporal score.
     */
    temporalScore?: number;

    /**
     * Environmental score
     */
    environmentalScore?: number;
}

/**
 * The list of possible CVSS Metric names. This list is based off CVSS 3.1
 */
export enum CVSS31MetricName {
    attackVector = "AV",
    attackComplexity = "AC",
    priviligesRequired = "PR",
    userInteraction = "UI",
    confidentiality = "C",
    integrity = "I",
    availability = "A",
    scope = "S",
    exploitCodeMaturity = "E",
    remediationLevel = "RL",
    reportConfidence = "RC",
    modifiedBaseMetrics = "?",
    confidentialityRequirement = "CR",
    integrityRequirement = "IR",
    availabilityRequirement = "AR",
    modifiedAttackVector = "MAV",
    modifiedAttackComplexity = "AC",
    modifiedPriviligesRequired = "PR",
    modifiedUserInteraction = "UI",
    modifiedConfidentiality = "C",
    modifiedIntegrity = "I",
    modifiedAvailability = "A",
    modifeidScope = "S"
}

/**
 * A coarse severity level for signal.
 */
export enum SeverityLevel {
    critical = "critical",
    high = "high",
    medium = "medium",
    low = "low",
    info = "info"
}

/**
 * These are the list of different types of Cyber Security types of signals supported by this specification.
 * The enum type is specifically string and not an auto-increment number. This is because dealing with numbers
 * makes data-lake queries and reports hard to understand. Numeric values are useful for machines and highly
 * responsive applications.
 */
export enum SecurityType {
    finding = "finding",
    outsideIn = "outsideIn",
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
}

/**
 * Points to an asset management system where more details about the entity could be found.
 *
 * Example: an ldap server, an ADFS server, an employee management system or an Identity management system
 */
export interface EntityManagement {
    /**
     * Type of the management system. Example: ldap
     */
    type: string;

    /**
     * Connection string, if it can be referenced by a connection string
     */
    connectionString?: string;

    /**
     * A set of name value pairs that can be used to infer the connection details to the system.
     */
    lookupAttributes: { [key: string]: string };
}

/**
 * Attributes that describe the asset
 */
export interface EntityAttributes {
    /**
     * One or more IP address interfaces the asset has. A machine can have multiple interfaces as seen from the following commands
     * in Windows/Unix
     * ```
     * ifconfig
     * ```
     * or
     * ```
     * ipconfig /all
     * ```
     */
    ipAddresses?: IpAddress[];

    /**
     * The type of machine, file or identity.
     * For a machine this will be the operating system name or the service name
     * Example: "Windows 10", "AWS DynamoDb"
     *
     * For an identity, it will be the type of identity.
     * Example: user, group, role, phone, email
     *
     * For a file, it will be the fileType.
     * Example: .pdf, .exe
     */
    type?: string;

    /**
     * The criticality of the asset from the business perspective.
     */
    criticality?: SeverityLevel;

    /**
     * In information security, confidentiality "is the property, that information is not made available or disclosed to
     * unauthorized individuals, entities, or processes.
     * This field captures the confidentiality requirement of the entity for which the signal is referring to.
     */
    confidentialityRequirement?: SeverityLevel;

    /**
     * In IT security, data integrity means maintaining and assuring the accuracy and completeness of data over its entire lifecycle.
     * This field captures the integrity requirement of the entity for which the signal is referring to.
     */
    integrityRequirement?: SeverityLevel;

    /**
     * For any information system to serve its purpose, the information must be available when it is needed.
     * This field captures the availability requirement of the entity for which the signal is referring to.
     */
    availabilityRequirement?: SeverityLevel;

    /**
     * A place holder to add name value pairs as tags/labels.
     */
    tags?: { [key: string]: string[] };
}

/**
 * Represent an IP address interface
 */
export interface IpAddress {
    /**
     * interface name, Example: eth0, Wifi
     */
    name: string;

    /**
     * An IP v4 in string format. It does not need to be an int32 for convenience
     */
    ipv4?: string;

    /**
     * An IP v6 string.
     */
    ipv6?: string;
}

/**
 * This is a SAFE specific section which is needed for supporting SAFE managed asset on-boarding and assessment from SAFE.
 * This contains settings needed to remotely connect to the machine to perform a remote assessment.
 */
export interface ConnectionAttributes {
    /**
     * Type of connection. Example: ssh, pim, db
     */
    type: string;

    /**
     * When connection can be represented via a connection string. Example: Db connection string
     */
    connectionString?: string;

    /**
     * The login username
     */
    username?: string;

    /**
     * The login password
     */
    password?: string;

    /**
     * Some Unix like machines have a second password
     */
    privilegedPassword?: string;

    /**
     * The remote port to connect to.
     */
    port?: number;

    /**
     * SSH key, if applicable
     */
    sshKey?: string;

    /**
     * SSH passphrase for the ssh key, if applicable
     */
    sshPassphrase?: string;

    /**
     * Any additional properties needed to establish the connection
     */
    attributes?: { [key: string]: string };
}

/**
 * A signal should always refer to an object on which the security threat applies to.
 *
 * It can be a machine, machines in the form of hostname(s), IP addresses (IPv4 and IPv6)
 * or a network CIDR or even a set of diverse resources like a cloud account (an ARN as an example)
 *
 * A file which is generally the object when reporting a malware.
 *
 * A user which is not necessarily a human user. This is typically characterized by an email id or a system user id.
 */
export enum EntityType {
    machine = "machine",
    file = "file",
    identity = "identity",
    organization = "organization"
}

type SignalUrl = string;

/**
 * The signal specification allows submitting a complete signal (with entity and security context) which
 * is the default use case.
 *
 * For the use case of daily assessments, there are information which are duplicate. Signal specification
 * allows the flexibility to the signal submitter to choose to post asset details alone which can later be
 * used as a reference in full signal submission.
 *
 * Example: A submitter would need to post a few 100 signals
 * for an asset on a daily basis. It is not needed to submit full details about the asset each time. This
 * approach is motivated by Azure and GCP security center's approach where they have the concept of resources
 * and findings as separate objects.
 *
 * A signal of type=entityOnly is a resource with an unique id. A signal
 * of type=securityContext is a Finding with a unique id and referencing an entity.
 */
export enum SignalType {
    default = "default",
    entityOnly = "entityOnly",
    securityContextOnly = "securityContextOnly"
}

/**
 * Possible effect of the signal. The values are motivated by AWS ASFF spec
 */
export enum Effect {
    dataExposure = "dataExposure",
    dataExfiltration = "dataExfiltration",
    dataDestruction = "dataDestruction",
    denialOfService = "denialOfService",
    resourceConsumption = "resourceConsumption"
}

/**
 * The status of the signal signifies the state of the security information. It can vary depending on the
 * type of signal.
 *
 * Example: A Compliance signal must either be Pass, Fail or NotAssessed
 *
 * Customer often mark status based on their business workflow. These are captured under workflow status.
 * Example: "AcceptedFailed, Archived,"
 */
export interface Status {
    complianceStatus?: ComplianceStatus;
    workflowStatus?: WorkflowStatus;
}

/**
 * Status applicable to compliance signals.
 */
export enum ComplianceStatus {
    pass = "pass",
    fail = "fail",
    unknown = "unknown"
}

/**
 * Status applicable to capture workflow state from the source of signal.
 */
export enum WorkflowStatus {
    new = "new",
    riskAccepted = "riskAccepted",
    resolved = "resolved"
}

/**
 * To accommodate a way to capture evidence of the security context.
 * Example: A reference to a screenshot or windows registry content that proves a missing configuration
 */
export interface Evidence {
    /**
     * To accommodate evidence in the form of text, comments, etc
     * Example: Registry or config file values or Software versions list.
     */
    observationText?: string;

    /**
     * To accommodate references to file based evidences
     * Example: An PDF document or a screenshot stored elsewhere.
     */
    path?: string;
}

export interface SecurityContext{
    /**
     * The most important aspect of a single is the type of security context. It can be any of the following types or something that the
     * signal consumer can interpret.
     *
     * Some common types of signals that impact scoring, are, but not limited to:
     * - Vulnerability (NVD CVEs and custom)
     * - Mis-Configuration info (CIS and custom)
     * - Endpoint Detection and Response (malware, virus, etc)
     * - Backup Success/Failure events from Backup and Recovery Software
     * - IDP/IPS/NAC/Firewall events
     * - DLP events
     * - Email Security events
     * - SIEM events
     *
     * So, the type of the event is a mandatory security context.
     */
    type: SecurityType;

    /**
     * To provide additional context, subType is an optional field.
     * Examples: 
     * "UserAccess" can be a subType for type=uba
     * "Blocked" can be a subType for type=firewall
     */
    subType?: string;

    /**
     * The status of the signal.
     */
    status: Status;

    /**
     * To accommodate a way to capture evidence of the security context.
     * Example: A reference to a screenshot or windows registry content that proves a missing configuration
     */
    evidence?: Evidence;

    /**
     * The severity of the signal is the most important aspect used in CRQ. Multiple vendors use their own severity system which
     * make it very hard to consolidate various signals from various security tools. Common variations seen among popular security
     * vendors include
     *
     * - 0-100 score
     * - 0-10 score
     * - 0-<no high value>
     * - Low, Medium, High, Critical.
     * - 0-5
     *
     * To add to complexity, a high number in some systems indicate high severity, whereas in others its the reverse.
     *
     * The most popular standard widely used across the industry are
     *
     * - CVSS - to communicate characteristics and severity of software vulnerabilities
     * - CCSS - based on CVSS, but to characterize software security configuration issues.
     *
     * Non VA and CA systems or for that matter, for reporting any security incidents, usage of CVSS to characterize the incident
     * severity is commonly used in the industry.
     *
     * The signal specification allows full characterization using CVSS/CCSS and yet allows any custom score.
     */
    severity: {
        /**
         * Indicates the type for severity. Example -cvss, ccss, custom
         */
        type: string;

        /**
         * A numeric value that indicates the severity. Its range will vary based on the type.
         *
         * Example: if type=cvss, the range will be from 0-10
         */
        value?: number;

        /**
         * Typically in the absence of a CVSS score, a coarse value is used here.
         * This may be present even when a specific score is present.
         *
         * Example: A CVSS rating of 9.5 is considered to have a Severity Level = High
         */
        level?: SeverityLevel;

        /**
         * The Common Vulnerability Scoring System based score.
         */
        cvss?: CVSS;
    };

    /**
     * When a signal maps to certain standard, like CIS, NVD, OWASP, etc, the mapping can be provided
     */
    standardsMapping?: StandardMapping[];

    /**
     * A list of relevant kill chains this signal contributes to.
     */
    killChainPhases?: KillChainPhase[];

    /**
     * A list of relevant attack patterns this signal contributes to.
     */
    attackPattern?: AttackPattern[];

    /**
     * A list of relevant cam controls this signal contributes to.
     */
    camControls?: string[];

    /**
     * See {@link killChainPhases}
     */
    campaign?: Campaign[];

    /**
     * To accommodate High Impact control values.
     * Range from -10 to +10
     * A negative impact translates to improvement in scoring
     * A positive impact translates to penalty of score.
     *
     * 0= neutral (use severity to determine impact)
     * 1=Low
     * 2=Low-Medium
     * 3=Medium
     * 3=Medium-High
     * 4=High
     * 5=High-Critical
     * 6=Critical
     * 7=Urgent
     * 8=BreachCertain
     * 9=BreachConfirmed
     */
    degreeOfImpact?: number;

    /**
     * The possible effect of this signal.
     */
    effect?: Effect[];

    /**
     * The type of control.
     */
    controlType?: ControlType;

    /**
     * Description of the security details of this signal.
     */
    description?: {
        /**
         * A paragraph describing the signal where the target is the business user
         */
        business?: string | SignalUrl;

        /**
         * A paragraph describing the signal where the target is the technical user
         */
        technical?: string | SignalUrl;

        /**
         * A paragraph describing the business impact of this signal.
         */
        businessImpact?: string | SignalUrl;
    };

    /**
     * If this is a signal that has relevant remediation steps. They are to be described here
     */
    remediation?: {
        /**
         * Description of the remediation.
         */
        description?: string | SignalUrl;

        /**
         * Any any references to web sites or documents
         */
        reference?: string | SignalUrl;
        
        /**
         * We will be deprecating 'reference' and use 'references' to support multiple reference links in future
         */
        references?: (string | SignalUrl)[];

        /**
         * Describes the impact of remediation
         */
        impact?: string | SignalUrl;
    };
    /**
     * A place holder to add name value pairs as tags/labels.
     */
    tags?: { [key: string]: string[] };
}
