import { qualityOfSignal } from "../../src";
import {
  ComplianceStatus,
  EntityType,
  SecurityType,
  SeverityLevel,
  Signal,
  SignalType,
  WorkflowStatus,
} from "../../src/interfaces/signal";

const poorSignal: Signal = {
  version: "1.0",
  id: "09d34300-4c54-4e5e-9050-fff5d912cb19",
  name: "External accounts with owner permissions should be removed from your subscription",
  source: { name: "uat.safescore.io", nextSubmissionIntervalInMins: 1440 },
  type: "default" as SignalType,
  description:
    "External accounts in Azure AD are accounts having different domain names than the one which is being used in corporate identities (such as Azure AD B2B collaboration, Microsoft Accounts, etc.). Usually, these accounts are not managed or monitored by the organization and can be targets for attackers looking to find ways to access the data without being noticed. External accounts with owner privileges should be removed from the subscription in order to prevent unmonitored access.<br>(Related policy: External accounts with owner permissions should be removed from your subscription)",
  createdAt: new Date("2022-07-22T02:15:05.000Z"),
  confidence: 100,
  securityContext: {
    type: "ca" as SecurityType,
    status: { complianceStatus: ComplianceStatus.fail },
    severity: {
      type: "ccss",
      value: 9.2,
      level: SeverityLevel.critical,
      cvss: {
        version: "3.0",
        baseScore: 9.2,
        vector: "AV:A/AC:H/PR:L/UI:R/S:C/C:L/I:L/A:L",
        temporalScore: 9.2,
        environmentalScore: 9.2,
      },
    },
    standardsMapping: [],
    description: {
      technical:
        "External accounts in Azure AD are accounts having different domain names than the one which is being used in corporate identities (such as Azure AD B2B collaboration, Microsoft Accounts, etc.). Usually, these accounts are not managed or monitored by the organization and can be targets for attackers looking to find ways to access the data without being noticed. External accounts with owner privileges should be removed from the subscription in order to prevent unmonitored access.<br>(Related policy: External accounts with owner permissions should be removed from your subscription)",
      businessImpact:
        "If external accounts with owner permissions are not removed from the subscription, then an attacker or an unauthorized user can gain access to the account from outside the domain, thus increasing the attack surface. With owner permissions, the attacker will be able to view sensitive data without being noticed (as these accounts are not managed or monitored by the organization) and put confidentiality of the account at risk, assign roles to the malicious accounts or manipulate the data present in the account making it unavailable for use.",
    },
    remediation: {
      description:
        "1. Login to the Azure portal, go to \"External accounts with owner permissions should be removed from your subscription\" recommendation in the Security Center.<br>2. Click a subscription from the list of subscriptions below or click 'Take action' if you are coming from a specific subscription.<br>3. The list of external user accounts that require access removal opens.<br>4. Click 'Continue'. The Access control (IAM) page opens.<br>5. In the Access control page:<br>    a. Click the 'Role assignments'<br>    b. Search and select the users that were in the list of user accounts that require removal. You can scroll back to the left to see the list.<br>    c. Click 'Remove'.",
      reference:
        "https://techcommunity.microsoft.com/t5/azure-security-center/security-controls-in-azure-security-center-manage-access-and/ba-p/1720540",
      impact:
        "If any of the external account is being used by subscriptions, then removing it will revoke its permissions. So, if there is a business requirement to allow an external account to use subscriptions then we should not remove it.",
    },
    degreeOfImpact: 0,
    tags: {},
  },
};

const averageSignal: Signal = {
  version: "1.0",
  id: "09d34300-4c54-4e5e-9050-fff5d912cb19",
  name: "External accounts with owner permissions should be removed from your subscription",
  source: { name: "uat.safescore.io", nextSubmissionIntervalInMins: 1440 },
  type: "default" as SignalType,
  description:
    "External accounts in Azure AD are accounts having different domain names than the one which is being used in corporate identities (such as Azure AD B2B collaboration, Microsoft Accounts, etc.). Usually, these accounts are not managed or monitored by the organization and can be targets for attackers looking to find ways to access the data without being noticed. External accounts with owner privileges should be removed from the subscription in order to prevent unmonitored access.<br>(Related policy: External accounts with owner permissions should be removed from your subscription)",
  createdAt: new Date("2022-07-21T02:15:05.000Z"),
  confidence: 100,
  entity: {
    type: "machine" as EntityType,
    name: "Pay-As-You-Go",
    entityAttributes: {
      ipAddresses: [{ name: "" }],
      type: "Azure - Subscriptions",
      criticality: "medium" as SeverityLevel,
      confidentialityRequirement: "medium" as SeverityLevel,
      integrityRequirement: "medium" as SeverityLevel,
      availabilityRequirement: "medium" as SeverityLevel,
      tags: {
        instanceId: ["/subscriptions/ea002cb5-ae6d-435a-aeb6-8a47c8ccd96f"],
        cloudResourceId: ["ea002cb5-ae6d-435a-aeb6-8a47c8ccd96f"],
        cloudAccountId: ["Pay-As-You-Go"],
        region: ["NA"],
      },
    },
  },
  securityContext: {
    type: "ca" as SecurityType,
    status: { complianceStatus: ComplianceStatus.fail },
    severity: {
      type: "ccss",
      value: 9.2,
      level: SeverityLevel.critical,
      cvss: {
        version: "3.0",
        baseScore: 9.2,
        vector: "AV:A/AC:H/PR:L/UI:R/S:C/C:L/I:L/A:L",
        temporalScore: 9.2,
        environmentalScore: 9.2,
      },
    },
    standardsMapping: [],
    description: {
      technical:
        "External accounts in Azure AD are accounts having different domain names than the one which is being used in corporate identities (such as Azure AD B2B collaboration, Microsoft Accounts, etc.). Usually, these accounts are not managed or monitored by the organization and can be targets for attackers looking to find ways to access the data without being noticed. External accounts with owner privileges should be removed from the subscription in order to prevent unmonitored access.<br>(Related policy: External accounts with owner permissions should be removed from your subscription)",
      businessImpact:
        "If external accounts with owner permissions are not removed from the subscription, then an attacker or an unauthorized user can gain access to the account from outside the domain, thus increasing the attack surface. With owner permissions, the attacker will be able to view sensitive data without being noticed (as these accounts are not managed or monitored by the organization) and put confidentiality of the account at risk, assign roles to the malicious accounts or manipulate the data present in the account making it unavailable for use.",
    },
    remediation: {
      description:
        "1. Login to the Azure portal, go to \"External accounts with owner permissions should be removed from your subscription\" recommendation in the Security Center.<br>2. Click a subscription from the list of subscriptions below or click 'Take action' if you are coming from a specific subscription.<br>3. The list of external user accounts that require access removal opens.<br>4. Click 'Continue'. The Access control (IAM) page opens.<br>5. In the Access control page:<br>    a. Click the 'Role assignments'<br>    b. Search and select the users that were in the list of user accounts that require removal. You can scroll back to the left to see the list.<br>    c. Click 'Remove'.",
      reference:
        "https://techcommunity.microsoft.com/t5/azure-security-center/security-controls-in-azure-security-center-manage-access-and/ba-p/1720540",
      impact:
        "If any of the external account is being used by subscriptions, then removing it will revoke its permissions. So, if there is a business requirement to allow an external account to use subscriptions then we should not remove it.",
    },
    degreeOfImpact: 0,
    tags: {},
  },
};

const goodSignal: Signal = {
  version: "1.0",
  id: "09d34300-4c54-4e5e-9050-fff5d912cb19",
  name: "External accounts with owner permissions should be removed from your subscription",
  source: { name: "uat.safescore.io", nextSubmissionIntervalInMins: 1440 },
  type: "default" as SignalType,
  description:
    "External accounts in Azure AD are accounts having different domain names than the one which is being used in corporate identities (such as Azure AD B2B collaboration, Microsoft Accounts, etc.). Usually, these accounts are not managed or monitored by the organization and can be targets for attackers looking to find ways to access the data without being noticed. External accounts with owner privileges should be removed from the subscription in order to prevent unmonitored access.<br>(Related policy: External accounts with owner permissions should be removed from your subscription)",
  createdAt: new Date("2022-07-21T02:15:05.000Z"),
  confidence: 100,
  entity: {
    type: "machine" as EntityType,
    name: "Pay-As-You-Go",
    entityAttributes: {
      ipAddresses: [{ name: "" }],
      type: "Azure - Subscriptions",
      criticality: "medium" as SeverityLevel,
      confidentialityRequirement: "medium" as SeverityLevel,
      integrityRequirement: "medium" as SeverityLevel,
      availabilityRequirement: "medium" as SeverityLevel,
      tags: {
        instanceId: ["/subscriptions/ea002cb5-ae6d-435a-aeb6-8a47c8ccd96f"],
        cloudResourceId: ["ea002cb5-ae6d-435a-aeb6-8a47c8ccd96f"],
        cloudAccountId: ["Pay-As-You-Go"],
        region: ["NA"],
      },
    },
  },
  securityContext: {
    type: "ca" as SecurityType,
    status: { complianceStatus: ComplianceStatus.fail },
    severity: {
      type: "ccss",
      value: 9.2,
      level: SeverityLevel.critical,
      cvss: {
        version: "3.0",
        baseScore: 9.2,
        vector: "AV:A/AC:H/PR:L/UI:R/S:C/C:L/I:L/A:L",
        temporalScore: 9.2,
        environmentalScore: 9.2,
      },
    },
    standardsMapping: [],
    description: {
      technical:
        "External accounts in Azure AD are accounts having different domain names than the one which is being used in corporate identities (such as Azure AD B2B collaboration, Microsoft Accounts, etc.). Usually, these accounts are not managed or monitored by the organization and can be targets for attackers looking to find ways to access the data without being noticed. External accounts with owner privileges should be removed from the subscription in order to prevent unmonitored access.<br>(Related policy: External accounts with owner permissions should be removed from your subscription)",
      businessImpact:
        "If external accounts with owner permissions are not removed from the subscription, then an attacker or an unauthorized user can gain access to the account from outside the domain, thus increasing the attack surface. With owner permissions, the attacker will be able to view sensitive data without being noticed (as these accounts are not managed or monitored by the organization) and put confidentiality of the account at risk, assign roles to the malicious accounts or manipulate the data present in the account making it unavailable for use.",
    },
    remediation: {
      description:
        "1. Login to the Azure portal, go to \"External accounts with owner permissions should be removed from your subscription\" recommendation in the Security Center.<br>2. Click a subscription from the list of subscriptions below or click 'Take action' if you are coming from a specific subscription.<br>3. The list of external user accounts that require access removal opens.<br>4. Click 'Continue'. The Access control (IAM) page opens.<br>5. In the Access control page:<br>    a. Click the 'Role assignments'<br>    b. Search and select the users that were in the list of user accounts that require removal. You can scroll back to the left to see the list.<br>    c. Click 'Remove'.",
      reference:
        "https://techcommunity.microsoft.com/t5/azure-security-center/security-controls-in-azure-security-center-manage-access-and/ba-p/1720540",
      impact:
        "If any of the external account is being used by subscriptions, then removing it will revoke its permissions. So, if there is a business requirement to allow an external account to use subscriptions then we should not remove it.",
    },
    attackPattern: [
      {
        name: "Password Brute Forcing",
        description: "See https://capec.mitre.org/data/definitions/49.html",
        sourceName: "capec",
        sourceId: "CAPEC-49",
        mapping: {
          techniqueName: "Brute Force",
          techniqueId: "T1110",
        },
      },
    ],
    degreeOfImpact: 0,
    tags: {
      tenantName: ["acme"],
      projects: ["rosi", "pi"],
    },
  },
  tags: {
    tenantName: ["acme"],
    projects: ["rosi", "pi"],
  },
};

const averageSignalWithInstances: Signal = {
  version: "1.2.13",
  id: "aa332918-3319-470f-a18b-e75e326c9d7d",
  type: SignalType.default,
  name: "SystemInfoDiscovery",
  source: {
    name: "A-unique-signal-submitter-name",
  },
  description:
    "A process gathered information about the operating system or hardware. Adversaries can use this to identify system vulnerabilities. Review the process tree.",
  entity: {
    type: EntityType.machine,
    name: "MyVirtualMachine.acme.com",
    entityAttributes: {
      ipAddresses: [
        {
          name: "Ip",
          ipv4: "10.0.6.173",
        },
      ],
      type: "Windows Server 2019 Datacenter 64 bit Edition Version 1809 Build 17763",
      tags: {
        hostname: ["MyVirtualMachine"],
      },
    },
  },
  securityContexts: [
    {
      type: SecurityType.edr,
      status: {
        complianceStatus: ComplianceStatus.fail,
        workflowStatus: WorkflowStatus.new,
      },
      evidence: {
        observationText: "malware.exe tries to access system info",
      },
      severity: {
        type: "custom",
        level: SeverityLevel.high,
      },
      attackPattern: [
        {
          name: "Data from Local system",
          mapping: {
            techniqueName: "Data from Local system",
            techniqueId: "T1005",
          },
          sourceName: "ATT&CK",
        },
      ],
      tags: {
        detectionId: ["999e1e2a-effc-4317-b7b3-31e184d15481"],
        detectionTime: ["2023-03-09T11:42:28Z"],
        files: [
          '{"name":"malware.exe","checksumSha256":"550a68076cd1bade01da6e7a359d5642d1222934a1a862f5045e17374ef89539","checksumMd5":"78979bd9288153580175da12d95f05b5","filePath":"/usr/bin/malware.exe","commandLine":"/usr/bin/malware.exe --systemd-watchdog","parentchecksumSha256":"","parentchecksumMd5":""}',
          '{"name":"malwareSubProcess.exe","checksumSha256":"9900a68076cd1bade01da6e7a359d5642d1222934a1a862f5045e17374ef89539","checksumMd5":"87179bd9288153580175da12d95f05b5","filePath":"/usr/bin/malwareSubProcess.exe","commandLine":"/usr/bin/malwareSubProcess.exe --systemd-watchdog","parentchecksumSha256":"","parentchecksumMd5":""}',
        ],
      },
    },
    {
      type: SecurityType.edr,
      status: {
        complianceStatus: ComplianceStatus.fail,
        workflowStatus: WorkflowStatus.new,
      },
      evidence: {
        observationText: "someothermalware.exe",
      },
      severity: {
        type: "custom",
        level: SeverityLevel.high,
      },
      attackPattern: [
        {
          name: "Data from Local system",
          mapping: {
            techniqueName: "Data from Local system",
            techniqueId: "T1005",
          },
          sourceName: "ATT&CK",
        },
      ],
      tags: {
        detectionId: ["111e1e2a-effc-4317-b7b3-31e184d15481"],
        detectionTime: ["2023-03-09T11:42:28Z"],
        files: [
          '{"name":"someothermalware.exe","checksumSha256":"550a68076cd1bade01da6e7a359d5642d1222934a1a862f5045e17374ef89539","checksumMd5":"78979bd9288153580175da12d95f05b5","filePath":"/usr/bin/someothermalware.exe","commandLine":"/usr/bin/someothermalware.exe --systemd-watchdog","parentchecksumSha256":"","parentchecksumMd5":""}',
        ],
      },
    },
  ],
  firstSeen: new Date("2023-03-08T11:42:28Z"),
  lastSeen: new Date("2023-03-09T11:42:28Z"),
  confidence: 100,
  createdAt: new Date("2023-03-09T11:42:28Z")
};

describe("Test quality of signals for various types of signals", () => {
  it("should check that a average quality signal is indeed gets a quality value less than 33", async () => {
    const quality = qualityOfSignal(poorSignal);
    console.log(`Quality = ${quality}`);
    expect(quality).toBeLessThan(33);
  });

  it("should check that a average quality signal is indeed gets a quality value greater than 33 and less than 66", async () => {
    const quality = qualityOfSignal(averageSignal);
    console.log(`Quality = ${quality}`);
    expect(quality).toBeLessThan(66);
    expect(quality).toBeGreaterThan(33);
  });

  it("should check that a average quality signal is indeed gets a quality value greater than 66", async () => {
    const quality = qualityOfSignal(goodSignal);
    console.log(`Quality = ${quality}`);
    expect(quality).toBeGreaterThan(66);
  });

  it("should check that a average quality signal containing instances is indeed gets a quality value greater than 33 and less than 66", async () => {
    const quality = qualityOfSignal(averageSignalWithInstances);
    console.log(`Quality = ${quality}`);
    expect(quality).toBeLessThan(66);
    expect(quality).toBeGreaterThan(33);
  });
});
