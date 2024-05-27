import { Breadcrumb } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

interface Props {
  courseId: string;
  courseTitle: React.ReactNode;
  title: React.ReactNode;
}

export function CourseBreadcrumb({ courseId, courseTitle, title }: Props) {
  const { t } = useTranslation();

  return (
    <Breadcrumb
      className="mb-breadcrumb"
      items={[
        {
          title: <Link to="/courses">{t('my_courses_page.head_title')}</Link>,
        },
        {
          title: <Link to={`/courses/${courseId}`}>{courseTitle}</Link>,
        },
        {
          title,
        },
      ]}
    />
  );
}
