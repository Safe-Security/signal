import {
  ComplianceStatus,
  ControlType,
  EntityType,
  SecurityType,
  SeverityLevel,
  Signal
} from "../interfaces/signal";

export const getHighQualityVASignal = (): Signal => {
  const signal: Signal = {
    version: "1.0",
    id: "09d34300-4c54-4e5e-9050-fff5d912cb19",
    name: "rpc.py through 0.6.0 allows Remote Code Execution",
    source: {
      name: "A-unique-signal-submitter-name",
      nextSubmissionIntervalInMins: 1440
    },
    description:
      'rpc.py through 0.6.0 allows Remote Code Execution because an unpickle occurs when the "serializer: pickle" HTTP header is sent. In other words, although JSON (not Pickle) is the default data format, an unauthenticated client can cause the data to be processed with unpickle',
    firstSeen: new Date("2022-08-07"),
    createdAt: new Date(),
    entity: {
      type: EntityType.machine,
      name: "MyVirtualMachine.acme.com",
      entityAttributes: {
        criticality: SeverityLevel.medium,
        confidentialityRequirement: SeverityLevel.medium,
        integrityRequirement: SeverityLevel.critical,
        availabilityRequirement: SeverityLevel.high,
        tags:{
          location: ["Mumbai"],
          dept: ["Finance"],
          os: ["CentOS 7"]
        }
      }
    },

    securityContext: {
      type: SecurityType.va,
      status: { complianceStatus: ComplianceStatus.fail },
      severity: {
        type: "cvss",
        cvss: {
          version: "3.1",
          baseScore: 9.8,
          vector: "CVSS:3.1/AV:N/AC:L/PR:N/UI:N/S:U/C:H/I:H/A:H"
        }
      },
      standardsMapping: [{ name: "cve", value: "CVE-2022-35411" }],
      attackPattern: [
        {
          name: "Compromise Accounts",
          sourceName: "ATT&CK",
          mapping: {
            techniqueName: "Compromise Accounts",
            techniqueId: "T1586"
          }
        },
        {
          name: "Endpoint Denial of Service",
          sourceName: "ATT&CK",
          mapping: {
            techniqueName: "Endpoint Denial of Service",
            techniqueId: "T1499"
          }
        },
        {
          name: "Exploitation of Remote Services",
          sourceName: "ATT&CK",
          mapping: {
            techniqueName: "Exploitation of Remote Services",
            techniqueId: "T1210"
          }
        }
      ],
      controlType: ControlType.detection,
      description: {
        technical: "A suitable technical description",
        businessImpact: "The potential business impact of this vulnerability"
      },
      remediation: {
        description: "Describe how to remediate",
        reference: "http://packetstormsecurity.com/files/167872/rpc.py-0.6.0-Remote-Code-Execution.html",
        impact: "Describe the impact of remediation"
      },
      tags:{
        resource: ["exploit","Patch"],
        configuration:["cpe:2.3:a:rpc.py_project:rpc.py:*:*:*:*:*:*:*:*"]
      }
    },
    tags:{
      application: ["rpc.py"],
      port: ["443", "80"]
    }
  };
  return signal;
};
