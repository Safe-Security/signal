import {
  ComplianceStatus,
  EntityType,
  SecurityType,
  SeverityLevel,
  Signal,
  SignalType,
  WorkflowStatus,
} from "../interfaces/signal";

export const getHighQualityEDRSignal = (): Signal => {
  const signal: Signal = {
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
    createdAt: new Date("2023-03-09T11:42:28Z"),
  };
  return signal;
};
