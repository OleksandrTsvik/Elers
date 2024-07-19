import react from '@vitejs/plugin-react';
import { defineConfig } from 'vite';
import commonjs from 'vite-plugin-commonjs';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    react(),
    // eslint-disable-next-line @typescript-eslint/no-unsafe-call
    commonjs({
      filter(id: string) {
        if (['ckeditor5/build/ckeditor.js'].includes(id)) {
          return true;
        }
      },
    }),
  ],
  server: {
    port: 3000,
  },
  optimizeDeps: {
    include: ['ckeditor5-custom-build'],
  },
  build: {
    outDir: '../Api/wwwroot/',
    target: 'ES2022',
    commonjsOptions: {
      include: [/ckeditor5-custom-build/, /node_modules/],
    },
  },
  base: '/',
});
