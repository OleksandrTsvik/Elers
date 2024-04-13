import { ThemeConfig, theme } from 'antd';

import { HEADER_HEIGHT } from '../../layout/layout.constants';
import { ColorMode } from '../../store/color-mode.slice';

export default function configTheme(mode: ColorMode): ThemeConfig {
  return {
    algorithm: mode === 'dark' ? theme.darkAlgorithm : theme.defaultAlgorithm,
    components: {
      Layout: {
        headerHeight: HEADER_HEIGHT,
      },
    },
  };
}
