import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import { useUpdateCourseDescriptionMutation } from '../../api/courses.api';
import { EditableText } from '../../common';
import { COURSE_RULES } from '../../common/rules';

import styles from './course-edit.module.scss';

interface Props {
  courseId: string;
  description?: string;
}

export default function CourseEditDescription({
  courseId,
  description,
}: Props) {
  const { t } = useTranslation();

  const [updateCourse, { isLoading, error }] =
    useUpdateCourseDescriptionMutation();

  const handleChange = async (value: string) => {
    await updateCourse({ id: courseId, description: value }).unwrap();
  };

  return (
    <EditableText
      type="textarea"
      text={description}
      loading={isLoading}
      changeText={t('course_edit_page.change_description')}
      label={t('course.description')}
      inputProps={{ maxLength: COURSE_RULES.description.max }}
      error={error}
      onChange={handleChange}
    >
      <Typography.Paragraph className={styles.description} type="secondary">
        {description}
      </Typography.Paragraph>
    </EditableText>
  );
}
