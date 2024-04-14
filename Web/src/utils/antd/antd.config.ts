import { ThemeConfig, theme } from 'antd';
import { ConfigOptions } from 'antd/es/message/interface';
import { NotificationConfig } from 'antd/es/notification/interface';

import { HEADER_HEIGHT } from '../../layout/layout.constants';
import { ColorMode } from '../../store/color-mode.slice';

export function configTheme(mode: ColorMode): ThemeConfig {
  return {
    algorithm: mode === 'dark' ? theme.darkAlgorithm : theme.defaultAlgorithm,
    components: {
      Layout: {
        headerHeight: HEADER_HEIGHT,
      },
    },
  };
}

export const messageConfig: ConfigOptions = {};

export const notificationConfig: NotificationConfig = {
  placement: 'bottomRight',
};
