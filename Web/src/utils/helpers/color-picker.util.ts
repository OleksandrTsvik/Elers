import { presetPalettes } from '@ant-design/colors';
import { Color } from 'antd/es/color-picker';

export type ColorPickerValue = Color | string | null;

export interface DefaultPresetsItem {
  label: string;
  colors: string[] & {
    primary?: string;
  };
  defaultOpen?: boolean;
}

export function generatePresets(presets = presetPalettes) {
  return Object.entries(presets).map<DefaultPresetsItem>(([label, colors]) => ({
    label,
    colors: [...new Set(colors)],
  }));
}

export function parseColorPickerValue(
  color: ColorPickerValue | undefined,
): string | undefined {
  if (!color) {
    return;
  }

  if (typeof color === 'string') {
    return color;
  }

  if (color.cleared) {
    return;
  }

  return color.toHexString();
}
