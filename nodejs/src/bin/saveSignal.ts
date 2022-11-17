import { writeFileSync } from "fs";
import path from "path";

import { Signal } from "../interfaces/signal"

export const saveSignal= (signal:Signal, filename: string):string => {
    const fullPath = path.join(process.env.SIGNAL_SAMPLES_DIR||"./", filename);
    writeFileSync(fullPath, JSON.stringify(signal, null, 2),"utf-8");
    return fullPath;
}