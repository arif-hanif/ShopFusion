import { defineConfig } from "vitest/config";

export default defineConfig({
  test: {
    setupFiles: ["src/setup-tests.ts"],
    coverage: {
      provider: "v8",
    },
  },
  resolve: {
    alias: {
      "@": "/src",
    },
  },
});