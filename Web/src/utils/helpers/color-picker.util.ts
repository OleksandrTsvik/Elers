import { presetPalettes } from '@ant-design/colors';
import { Color } from 'antd/es/color-picker';

import { getUniqueArrayItems } from './type-converters.util';
import { isString } from './type-guards.util';

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
    colors: getUniqueArrayItems(colors),
  }));
}

export function parseColorPickerValue(
  color: ColorPickerValue | undefined,
): string | undefined {
  if (!color) {
    return;
  }

  if (isString(color)) {
    return color;
  }

  if (color.cleared) {
    return;
  }

  return color.toHexString();
}
