import { Breadcrumb } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

interface Props {
  courseId: string;
  title: string;
}

export default function CourseEditBreadcrumb({ courseId, title }: Props) {
  const { t } = useTranslation();

  return (
    <Breadcrumb
      className="mb-breadcrumb"
      items={[
        {
          title: <Link to="/">{t('course.courses')}</Link>,
        },
        {
          title: <Link to={`/courses/${courseId}`}>{title}</Link>,
        },
        {
          title: t('course_edit_page.title'),
        },
      ]}
    />
  );
}
