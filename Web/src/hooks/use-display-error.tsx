import { App } from 'antd';
import { useCallback } from 'react';

import { ErrorMessage } from '../components';

interface DisplayErrorConfig {
  displayType?: 'message' | 'notification';
  display?: boolean;
}

export default function useDisplayError() {
  const { message, notification } = App.useApp();

  const displayError = useCallback(
    (
      error: unknown,
      { displayType, display }: DisplayErrorConfig = {
        displayType: 'message',
        display: true,
      },
    ): void => {
      if (!display) {
        return;
      }

      switch (displayType) {
        case 'notification':
          notification.error({ message: <ErrorMessage error={error} /> });
          break;
        default:
          void message.error(<ErrorMessage error={error} />);
      }
    },
    [notification, message],
  );

  return { displayError };
}
