import { Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import { useUpdateCourseDescriptionMutation } from '../../api/courses.api';
import { EditableText } from '../../shared';
import { COURSE_RULES } from '../../shared/rules';

import styles from './course-edit.module.scss';

interface Props {
  courseId: string;
  description?: string;
  onUpdateDescription: (value: string) => void;
}

export default function CourseEditDescription({
  courseId,
  description,
  onUpdateDescription,
}: Props) {
  const { t } = useTranslation();

  const [updateCourse, { isLoading, error }] =
    useUpdateCourseDescriptionMutation();

  const handleChange = async (value: string) => {
    await updateCourse({ id: courseId, description: value })
      .unwrap()
      .then(() => onUpdateDescription(value));
  };

  return (
    <EditableText
      type="textarea"
      text={description}
      loading={isLoading}
      changeText={t('course_edit_page.change_description')}
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
