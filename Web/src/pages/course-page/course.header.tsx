import { Flex, Typography } from 'antd';

import useCourseActions from './use-course.actions';
import { AuthGuard } from '../../auth';
import { ResponsiveTitle } from '../../common/typography';
import { SettingsDropdown } from '../../components';

import styles from './course.module.scss';

interface Props {
  courseId: string;
  title: string;
  description?: string;
}

export default function CourseHeader({ courseId, title, description }: Props) {
  const { courseActions, isLoading } = useCourseActions(courseId, title);

  return (
    <>
      <Flex justify="space-between" gap="small">
        <ResponsiveTitle className={styles.title}>{title}</ResponsiveTitle>
        <AuthGuard>
          <SettingsDropdown items={courseActions} loading={isLoading} />
        </AuthGuard>
      </Flex>
      {description && (
        <Typography.Paragraph className={styles.description} type="secondary">
          {description}
        </Typography.Paragraph>
      )}
    </>
  );
}
