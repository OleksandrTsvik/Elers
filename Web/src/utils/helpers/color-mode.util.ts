import { ColorMode } from '../../store/color-mode.slice';

export function changeDocumentBodyColorMode(colorMode: ColorMode) {
  document.body.classList.toggle('dark', colorMode === 'dark');
  document.body.classList.toggle('light', colorMode === 'light');
}
