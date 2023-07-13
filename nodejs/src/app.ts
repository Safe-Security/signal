import axios, { AxiosResponse } from "axios";
import fs from "fs";
import FormData from "form-data";
import { Signal } from "./interfaces/signal";

const app = async () => {
  const username = process.env.SAFE_USERNAME as string;
  const password = process.env.SAFE_PASSWORD as string;
  const safeURL = process.env.SAFE_URL as string;
  const dir = process.env.SIGNAL_SAMPLES_DIR as string;

  const response = (await axios.post(
    `${safeURL}/api/v3/auth`,
    {},
    {
      auth: {
        username,
        password,
      },
    }
  )) as AxiosResponse<{ accessToken: string }>;

  console.log("Successfully fetched token");

  fs.readdir(dir, async (err, filenames) => {
    if (err) {
      console.log(err);
      return;
    }
    await processSignals(filenames, dir, safeURL, response.data.accessToken);
    await processZipSignals(filenames, dir, safeURL, response.data.accessToken);
  });
};

const processSignals = async (
  filenames: string[],
  dir: string,
  safeURL: string,
  accessToken: string
) => {
  filenames
    .filter((filename) => filename.endsWith(".json"))
    .map(async (filename) => {
      fs.readFile(dir + "/" + filename, "utf-8", async (err, content) => {
        if (err) {
          console.log(err);
          return;
        }
        const signalResponse = await axios.post(
          `${safeURL}/api/v3/signals/`,
          JSON.parse(content) as Signal,
          {
            headers: {
              Authorization: `Bearer ${accessToken}`,
              "Content-Type": "application/json",
            },
          }
        );
        console.log(`${JSON.stringify(signalResponse.data)} ${filename}`);
      });
    });
};

const processZipSignals = async (
  filenames: string[],
  dir: string,
  safeURL: string,
  accessToken: string
) => {
  filenames
    .filter((filename) => filename.endsWith(".zip"))
    .map(async (filename) => {
      const form = new FormData();
      console.log(`${dir}/${filename}`);
      form.append("file", fs.createReadStream(`${dir}/${filename}`));
      const signalResponse = await axios.post(
        `${safeURL}/api/v3/signals/zip`,
        form,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
            "Content-Type": "multipart/form-data",
          },
        }
      );
      console.log(`${JSON.stringify(signalResponse.data)} ${filename}`);
    });
};

app();
