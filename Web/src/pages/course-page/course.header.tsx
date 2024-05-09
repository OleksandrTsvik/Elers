import { Flex, Typography } from 'antd';

import useCourseActions from './use-course.actions';
import { ResponsiveTitle } from '../../common';
import { SettingsDropdown } from '../../components';

import styles from './course.module.scss';

interface Props {
  courseId: string;
  title: string;
  description?: string;
}

export default function CourseHeader({ courseId, title, description }: Props) {
  const { getActionItems } = useCourseActions();

  return (
    <>
      <Flex justify="space-between" gap="small">
        <ResponsiveTitle className={styles.title}>{title}</ResponsiveTitle>
        <SettingsDropdown items={getActionItems(courseId)} />
      </Flex>
      {description && (
        <Typography.Paragraph className={styles.description} type="secondary">
          {description}
        </Typography.Paragraph>
      )}
    </>
  );
}
