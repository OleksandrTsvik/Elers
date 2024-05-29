import { Flex, Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import CourseCreationModalButton from './course-creation.modal-button';
import { PermissionType, PermissionsGuard } from '../../auth';

export default function MyCoursesTitle() {
  const { t } = useTranslation();

  return (
    <Flex
      justify="space-between"
      align="center"
      gap="large"
      wrap="wrap"
      style={{ marginBottom: 18 }}
    >
      <Typography.Title style={{ margin: 0 }}>
        {t('my_courses_page.title')}
      </Typography.Title>
      <PermissionsGuard permissions={PermissionType.CreateCourse}>
        <CourseCreationModalButton />
      </PermissionsGuard>
    </Flex>
  );
}
