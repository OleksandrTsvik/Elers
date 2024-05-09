import { App } from 'antd';

import { ErrorMessage } from '../common';

interface DisplayErrorConfig {
  displayType?: 'message' | 'notification';
  display?: boolean;
}

export default function useDisplayError() {
  const { message, notification } = App.useApp();

  const displayError = (
    error: unknown,
    { displayType, display }: DisplayErrorConfig = {
      displayType: 'message',
      display: true,
    },
  ): void => {
    if (display === false) {
      return;
    }

    switch (displayType) {
      case 'notification':
        notification.error({ message: <ErrorMessage error={error} /> });
        break;
      default:
        void message.error(<ErrorMessage error={error} />);
    }
  };

  return { displayError };
}
