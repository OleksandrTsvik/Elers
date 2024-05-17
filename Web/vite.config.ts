import react from '@vitejs/plugin-react';
import { defineConfig } from 'vite';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000,
  },
  optimizeDeps: {
    include: ['ckeditor5-custom-build'],
  },
  build: {
    commonjsOptions: {
      include: [/ckeditor5-custom-build/, /node_modules/],
    },
  },
  base: '/',
});
