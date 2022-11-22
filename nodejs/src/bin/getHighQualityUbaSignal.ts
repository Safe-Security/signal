/* eslint-disable comma-dangle */
import {
  ComplianceStatus,
  ControlType,
  EntityType,
  SecurityType,
  Signal
} from "../interfaces/signal";

export const getHighQualityUbaSignal = (): Signal => {
  const signal: Signal = {
    version: "1.0",
    id: "ff210e99-cd28-4a4a-a510-e0b865f6ce36",
    name: "Employee Personally Identifiable Information in the dark web",
    source: {
      name: "A-unique-signal-submitter-name",
      nextSubmissionIntervalInMins: 1440
    },
    description:
      "This control checks whether the Personally Identifiable Information of employees of an organization is available in the dark web. PII information includes the first name, last name, city, county, state, phone number and gender.",
    firstSeen: new Date("2022-08-07"),
    createdAt: new Date(),
    entity: {
      type: EntityType.identity,
      name: "john.doe@acme.com",
      entityAttributes: {
        type: "User",
        tags: {
          location: ["Mumbai"],
          dept: ["Finance"],
          os: ["CentOS 7"]
        }
      },
    },
    securityContext: {
      type: SecurityType.uba,
      status: { complianceStatus: ComplianceStatus.pass },
      evidence: {
        observationText: "No personal information breach detected"
      },
      severity: { type: "ccss", value: 8 },
      attackPattern: [
        {
          name: "Gather Victim Identity Information: Email Addresses",
          sourceName: "ATT&CK",
          mapping: {
            techniqueName:
              "Gather Victim Identity Information: Email Addresses",
            techniqueId: "T1589.002"
          },
        },
        {
          name: "Compromise Accounts: Email Accounts",
          sourceName: "ATT&CK",
          mapping: {
            techniqueName: "Compromise Accounts: Email Accounts",
            techniqueId: "T1586.002"
          }
        }
      ],
      controlType: ControlType.detection,
      description: {
        technical: "A suitable technical description",
        businessImpact: "The potential business impact of this vulnerability"
      },
      remediation: {
        description:
          "Closely monitor employee accounts with leaked PII data for identity theft",
        reference: "NA",
        impact: "NA"
      },
      tags:{
        resource: ["PII"]
      }
    },
    confidence: 100,
    tags: {
      stage: ["production"]
    },
  };
  return signal;
};
