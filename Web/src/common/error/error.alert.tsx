import { Alert, AlertProps, List } from 'antd';

import useParseError from '../../hooks/use-parse-error';

import styles from './error.module.scss';

interface Props extends AlertProps {
  error: unknown;
}

export default function ErrorAlert({ error, ...props }: Props) {
  const { message, description } = useParseError(error);

  return (
    <Alert
      type="error"
      {...props}
      message={message}
      description={
        description.length > 0 && (
          <List
            size="small"
            dataSource={description}
            renderItem={(item) => (
              <List.Item className={styles.alert_listItem}>{item}</List.Item>
            )}
          />
        )
      }
    />
  );
}
