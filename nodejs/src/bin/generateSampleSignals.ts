/* eslint-disable no-console */
import { Signal } from "../interfaces/signal";
import { qualityOfSignal } from "../utils/qualityOfSignal";

import { getHighQualityCASignal } from "./getHighQualityCASignal";
import { getHighQualityVASignal } from "./getHighQualityVASignal";
import { getSimpleCASignal } from "./getSimpleCASignal";
import { getSimpleVASignal } from "./getSimpleVASignal";
import { saveSignal } from "./saveSignal";

const generateSignal = (signal: Signal, filename: string): void => {
  const savedFilename = saveSignal(signal, filename);
  const qos = Math.floor(qualityOfSignal(signal))
  console.log(`Saved to ${savedFilename}. Quality of signal = ${qos}%`)
}

(() => {
  //CA signals
  generateSignal(getSimpleCASignal(), "simple-ca-signal.json");
  generateSignal(getHighQualityCASignal(), "high-quality-ca-signal.json");

  //VA signals
  generateSignal(getSimpleVASignal(), "simple-va-signal.json");
  generateSignal(getHighQualityVASignal(), "high-quality-va-signal.json");

  //EDR signals


  //People signals
})();



