/* eslint-disable functional/immutable-data */
import { SecurityContext, Signal } from "../interfaces/signal";

/**
 * Determine the quality of the signal. The more information a signal contains,
 * the better the quality is.
 *
 * @param signal
 * @returns A number between 0 and 100.
 */
// eslint-disable-next-line sonarjs/cognitive-complexity
export const qualityOfSignal = (signal: Signal): number => {
    let factors: { value: number; weight: number }[] = [] as unknown as {
      value: number;
      weight: number;
    }[];

    signal.name
        ? factors.push({ value: 60, weight: 1 })
        : factors.push({ value: 20, weight: 1 });

    signal.source.name
        ? factors.push({ value: 50, weight: 1 })
        : factors.push({ value: 30, weight: 1 });

    signal.type
        ? factors.push({ value: 60, weight: 1 })
        : factors.push({ value: 10, weight: 1 });

    signal.confidence
        ? factors.push({ value: signal.confidence, weight: 0.5 })
        : factors.push({ value: 30, weight: 1 });

    signal.entity
        ? factors.push({ value: 60, weight: 1 })
        : factors.push({ value: -300, weight: 1 });

    signal.entity?.type
        ? factors.push({ value: 60, weight: 1 })
        : factors.push({ value: 10, weight: 1 });

    signal.entity?.entityAttributes?.type
        ? factors.push({ value: 60, weight: 1 })
        : factors.push({ value: 30, weight: 1 });

    signal.entity?.entityAttributes?.confidentialityRequirement
        ? factors.push({ value: 70, weight: 1 })
        : factors.push({ value: 30, weight: 1 });

    signal.entity?.entityAttributes?.integrityRequirement
        ? factors.push({ value: 70, weight: 1 })
        : factors.push({ value: 30, weight: 1 });

    signal.entity?.entityAttributes?.availabilityRequirement
        ? factors.push({ value: 70, weight: 1 })
        : factors.push({ value: 30, weight: 1 });

        if (signal.securityContext) {
          const securityContextfactors = getSecurityContextFactors(
            signal.securityContext
          );
          factors = [...factors, ...securityContextfactors];
        }
        if (signal.securityContexts) {
          signal.securityContexts.forEach((securityContext) => {
            const securityContextfactors =
              getSecurityContextFactors(securityContext);
            factors = [...factors, ...securityContextfactors];
          });
        }
    const values = factors.map(item => item.value);
    const weights = factors.map(item => item.weight);

    return weightedAverage(values, weights);
};

const weightedAverage = (values: number[], weights: number[]): number => {
    const [sum, weightSum] = weights.reduce(
        (acc, weight, index) => {
            acc[0] = acc[0] + values[index] * weight;
            acc[1] = acc[1] + weight;
            return acc;
        },
        [0, 0]
    );
    return sum / weightSum;
};
const getSecurityContextFactors = (
  securityContext: SecurityContext
): { value: number; weight: number }[] => {
    const newFactor:{ value: number; weight: number }[]=[];
  securityContext?.type
    ? newFactor.push({ value: 70, weight: 1 })
    : newFactor.push({ value: 10, weight: 1 });

  securityContext?.status.complianceStatus
    ? newFactor.push({ value: 70, weight: 1 })
    : newFactor.push({ value: 30, weight: 1 });

  securityContext?.severity
    ? newFactor.push({ value: 50, weight: 1 })
    : newFactor.push({ value: 30, weight: 1 });

  securityContext?.standardsMapping?.length
    ? newFactor.push({ value: 200, weight: 1 })
    : newFactor.push({ value: 50, weight: 1 });

  securityContext?.attackPattern &&
  securityContext.attackPattern[0].mapping?.techniqueId
    ? newFactor.push({ value: 200, weight: 1 })
    : newFactor.push({ value: 50, weight: 1 });

  securityContext?.degreeOfImpact
    ? newFactor.push({ value: 200, weight: 1 })
    : newFactor.push({ value: 50, weight: 1 });

  securityContext?.controlType
    ? newFactor.push({ value: 60, weight: 1 })
    : newFactor.push({ value: 50, weight: 1 });

  securityContext?.effect
    ? newFactor.push({ value: 200, weight: 1 })
    : newFactor.push({ value: 50, weight: 1 });
  return newFactor;
};

