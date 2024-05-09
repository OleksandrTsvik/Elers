import { useTranslation } from 'react-i18next';

import { generatePresets } from '../utils/helpers';

const colorPresets = generatePresets();

export default function useColorPresets(presets = colorPresets) {
  const { t } = useTranslation('translation', { keyPrefix: 'color' });

  return presets.map(({ label, colors, defaultOpen }) => ({
    label: t([label, 'unspecific']),
    colors,
    defaultOpen,
  }));
}
