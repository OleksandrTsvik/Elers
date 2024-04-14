import { List, Typography } from 'antd';

import useErrorMessage from '../../hooks/use-error-message';

interface Props {
  error: unknown;
}

export default function ErrorDetails({ error }: Props) {
  const { getErrorMessage } = useErrorMessage();

  const errorMessage = getErrorMessage(error);

  return (
    <>
      <Typography.Text>{errorMessage.message}</Typography.Text>
      {errorMessage.description && (
        <List
          size="small"
          dataSource={
            Array.isArray(errorMessage.description)
              ? errorMessage.description
              : [errorMessage.description]
          }
          renderItem={(item) => <List.Item>{item}</List.Item>}
        />
      )}
    </>
  );
}
