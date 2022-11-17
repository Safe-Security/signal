import {
  ComplianceStatus,
  ControlType,
  EntityType,
  SecurityType,
  SeverityLevel,
  Signal
} from "../interfaces/signal";

export const getHighQualityCASignal = ():Signal => {
  const signal: Signal = {
    version: "1.0",
    id: "09d34300-4c54-4e5e-9050-fff5d912cb19",
    name: "Ensure 'Windows Firewall: Domain: Firewall state' is set to 'On (recommended)'",
    source: {
      name: "A-unique-signal-submitter-name",
      nextSubmissionIntervalInMins: 1440
    },
    description:"Select On (recommended) to have Windows Firewall with Advanced Security use the settings for this profile to filter network traffic. If you select Off, Windows Firewall with Advanced Security will not use any of the firewall rules or connection security rules for this profile.",
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
      type: SecurityType.ca,
      status: { complianceStatus: ComplianceStatus.fail },
      severity: {
        type: "ccss",
        value: 7.2,
        cvss: {
          version: "2.0",
          vector:"AV:L/AC:H/Au:S/C:C/I:C/A:P/GEL:H/GRL:H/EC:C/EI:C/EA:P/CR:M/IR:M/AR:M/CDP:ND/LRL:H/LVP:M/PTV:M/",
          baseScore: 7.2,
          temporalScore: 7.2,
          environmentalScore: 7.2
        }
      },
      standardsMapping:[
        {
          name: "stig",
          value: "V-63399"
        },
        {
          name: "cisBenchMark", 
          value:"9.1.1"
        }
      ],
      attackPattern: [
        {
          name: "Account Manipulation",
          sourceName: "ATT&CK",
          mapping: {
            techniqueName: "Account Manipulation",
            techniqueId: "T1098"
          }
        }
      ],
      controlType: ControlType.detection,
      "description": {
        "technical": "A suitable technical description",
        "businessImpact": "The potential business impact of this vulnerability"
      },
      "remediation": {
        "description": "Describe how to remediate",
        "reference": "https://technet.microsoft.com/en-us/library/bb490626.aspx",
        "impact": "Describe the impact of remediation"
      },
      tags:{
        resource: ["exploit"]
      }
    },
    tags:{
      stage: ["production"]
    }
  };
  return signal;
};
