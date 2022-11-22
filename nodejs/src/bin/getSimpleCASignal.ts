import {
  ComplianceStatus,
  EntityType,
  SecurityType,
  Signal
} from "../interfaces/signal";

export const getSimpleCASignal = ():Signal => {
  const signal: Signal = {
    version: "1.0",
    id: "09d34300-4c54-4e5e-9050-fff5d912cb19",
    name: "Ensure 'Windows Firewall: Domain: Firewall state' is set to 'On (recommended)'",
    source: {
      name: "A-unique-signal-submitter-name",
      nextSubmissionIntervalInMins: 1440
    },
    description:"Select On (recommended) to have Windows Firewall with Advanced Security use the settings for this profile to filter network traffic. If you select Off, Windows Firewall with Advanced Security will not use any of the firewall rules or connection security rules for this profile.",
    createdAt: new Date(),
    entity: {
      type: EntityType.machine,
      name: "MyVirtualMachine.acme.com"
    },
    securityContext: {
      type: SecurityType.ca,
      status: { complianceStatus: ComplianceStatus.fail },
      severity: {
        type: "ccss",
        value: 7.2
      }
    }
  };
  return signal;
};
