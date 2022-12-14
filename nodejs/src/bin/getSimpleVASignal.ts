import { ComplianceStatus, EntityType, SecurityType, Signal } from "../interfaces/signal";


export const getSimpleVASignal = ():Signal => {
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
    createdAt: new Date(),
    entity: {
      type: EntityType.machine,
      name: "MyVirtualMachine.acme.com"
    },

    securityContext: {
      type: SecurityType.va,
      status: { complianceStatus: ComplianceStatus.fail },
      severity: {
        type: "cvss",
        value: 9.8
      },
      standardsMapping: [{ name: "cve", value: "CVE-2022-35411" }]
    }
  };
  return signal;
};
