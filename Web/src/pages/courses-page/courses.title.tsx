import { Flex, Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import CourseCreationModalButton from './course-creation.modal-button';

export default function CoursesTitle() {
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
        {t('courses_page.title')}
      </Typography.Title>
      <CourseCreationModalButton />
    </Flex>
  );
}