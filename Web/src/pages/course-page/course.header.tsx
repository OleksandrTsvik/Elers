import { Typography } from 'antd';

import styles from './course.module.scss';

interface Props {
  title: string;
  description?: string;
}

export default function CourseHeader({ title, description }: Props) {
  return (
    <>
      <Typography.Title>{title}</Typography.Title>
      {description && (
        <Typography.Paragraph className={styles.description} type="secondary">
          {description}
        </Typography.Paragraph>
      )}
    </>
  );
}
