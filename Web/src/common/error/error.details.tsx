import { Alert, AlertProps, List } from 'antd';

import styles from './error.module.scss';

export type ErrorDetailsProps = Omit<AlertProps, 'message' | 'description'>;

interface Props extends ErrorDetailsProps {
  message: string;
  description: string[];
}

export default function ErrorDetails({
  message,
  description,
  ...props
}: Props) {
  return (
    <Alert
      type="error"
      message={message}
      description={
        description.length && (
          <List
            size="small"
            dataSource={description}
            renderItem={(item) => (
              <List.Item className={styles.alert_listItem}>{item}</List.Item>
            )}
          />
        )
      }
      {...props}
    />
  );
}
