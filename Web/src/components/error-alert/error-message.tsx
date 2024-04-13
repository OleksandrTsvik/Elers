import { List } from 'antd';

import useErrorMessage from '../../hooks/use-error-message';

interface Props {
  error: unknown;
}

export default function ErrorMessage({ error }: Props) {
  const { getErrorMessage } = useErrorMessage();

  const errorMessage = getErrorMessage(error);

  if (typeof errorMessage === 'string') {
    return errorMessage;
  }

  return (
    <List
      size="small"
      dataSource={errorMessage}
      renderItem={(item) => <List.Item>{item}</List.Item>}
    />
  );
}
