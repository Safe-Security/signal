/* eslint-disable functional/immutable-data */
import { Signal } from "../interfaces/signal";

/**
 * Determine the quality of the signal. The more information a signal contains,
 * the better the quality is.
 *
 * @param signal
 * @returns A number between 0 and 100.
 */
// eslint-disable-next-line sonarjs/cognitive-complexity
export const qualityOfSignal = (signal: Signal): number => {
    const factors = [];

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

    signal.securityContext?.type
        ? factors.push({ value: 70, weight: 1 })
        : factors.push({ value: 10, weight: 1 });

    signal.securityContext?.status.complianceStatus
        ? factors.push({ value: 70, weight: 1 })
        : factors.push({ value: 30, weight: 1 });

    signal.securityContext?.severity
        ? factors.push({ value: 50, weight: 1 })
        : factors.push({ value: 30, weight: 1 });

    signal.securityContext?.standardsMapping?.length
        ? factors.push({ value: 200, weight: 1 })
        : factors.push({ value: 50, weight: 1 });

    signal.securityContext?.attackPattern &&
    signal.securityContext.attackPattern[0].mapping?.techniqueId
        ? factors.push({ value: 200, weight: 1 })
        : factors.push({ value: 50, weight: 1 });

    signal.securityContext?.degreeOfImpact
        ? factors.push({ value: 200, weight: 1 })
        : factors.push({ value: 50, weight: 1 });

    signal.securityContext?.controlType
        ? factors.push({ value: 60, weight: 1 })
        : factors.push({ value: 50, weight: 1 });

    signal.securityContext?.effect
        ? factors.push({ value: 200, weight: 1 })
        : factors.push({ value: 50, weight: 1 });

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
