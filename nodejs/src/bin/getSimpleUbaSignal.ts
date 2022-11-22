/* eslint-disable comma-dangle */
import {
  ComplianceStatus,
  EntityType,
  SecurityType,
  Signal
} from "../interfaces/signal";

export const getSimpleUbaSignal = (): Signal => {
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
    createdAt: new Date(),
    entity: {
      type: EntityType.identity,
      name: "john.doe@acme.com"
    },
    securityContext: {
      type: SecurityType.uba,
      status: { complianceStatus: ComplianceStatus.pass },
      severity: { type: "ccss", value: 8 },
    }
  };
  return signal;
};
