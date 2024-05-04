import { Flex, Typography } from 'antd';

import CourseActionsDropdown from './course-actions.dropdown';
import { ResponsiveTitle } from '../../shared';

import styles from './course.module.scss';

interface Props {
  courseId: string;
  title: string;
  description?: string;
}

export default function CourseHeader({ courseId, title, description }: Props) {
  return (
    <>
      <Flex justify="space-between" gap="small">
        <ResponsiveTitle className={styles.title}>{title}</ResponsiveTitle>
        <CourseActionsDropdown courseId={courseId} />
      </Flex>
      {description && (
        <Typography.Paragraph className={styles.description} type="secondary">
          {description}
        </Typography.Paragraph>
      )}
    </>
  );
}
