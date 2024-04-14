import { Typography } from 'antd';

import useErrorMessage from '../../hooks/use-error-message';

interface Props {
  error: unknown;
}

export default function ErrorMessage({ error }: Props) {
  const { getErrorMessage } = useErrorMessage();

  const errorMessage = getErrorMessage(error);

  return <Typography.Text>{errorMessage.message}</Typography.Text>;
}
