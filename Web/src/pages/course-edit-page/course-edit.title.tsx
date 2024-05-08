import { useTranslation } from 'react-i18next';

import { useUpdateCourseTitleMutation } from '../../api/courses.api';
import { EditableText, ResponsiveTitle } from '../../shared';
import { COURSE_RULES } from '../../shared/rules';

import styles from './course-edit.module.scss';

interface Props {
  courseId: string;
  title: string;
}

export default function CourseEditTitle({ courseId, title }: Props) {
  const { t } = useTranslation();

  const [updateCourse, { isLoading, error }] = useUpdateCourseTitleMutation();

  const handleChange = async (value: string) => {
    await updateCourse({ id: courseId, title: value }).unwrap();
  };

  return (
    <EditableText
      type="input"
      text={title}
      loading={isLoading}
      changeText={t('course_edit_page.change_title')}
      label={t('course.title')}
      inputProps={{ maxLength: COURSE_RULES.title.max }}
      textRules={[
        {
          required: true,
          min: COURSE_RULES.title.min,
          max: COURSE_RULES.title.max,
          message: t('course.rules.title_len', COURSE_RULES.title),
        },
      ]}
      error={error}
      onChange={handleChange}
    >
      <ResponsiveTitle className={styles.title}>{title}</ResponsiveTitle>
    </EditableText>
  );
}
