import * as path from "node:path";
import { type UserConfig } from "vite";
import react from "@vitejs/plugin-react-swc";
import relay from "vite-plugin-relay";

// https://vitejs.dev/config/
const config: UserConfig = {
  publicDir: "./public",
  plugins: [react(), relay],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
      "@generated": path.resolve(__dirname, "./src/__generated__"),
    },
  },
  build: {
    outDir: "build",
    chunkSizeWarningLimit: 3000,
  },
  server: {
    port: 3005,
  },
  preview: {
    port: 3005,
  },
};

export default config;