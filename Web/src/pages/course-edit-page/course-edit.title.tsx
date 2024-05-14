import { useTranslation } from 'react-i18next';

import { useUpdateCourseTitleMutation } from '../../api/courses.api';
import { COURSE_RULES } from '../../common/rules';
import { EditableText, ResponsiveTitle } from '../../common/typography';
import useValidationRules from '../../hooks/use-validation-rules';

import styles from './course-edit.module.scss';

interface Props {
  courseId: string;
  title: string;
}

export default function CourseEditTitle({ courseId, title }: Props) {
  const { t } = useTranslation();
  const { trimWhitespace } = useValidationRules();

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
        trimWhitespace,
      ]}
      error={error}
      onChange={handleChange}
    >
      <ResponsiveTitle className={styles.title}>{title}</ResponsiveTitle>
    </EditableText>
  );
}
