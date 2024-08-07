import { Breadcrumb } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

interface Props {
  courseId: string;
  courseTitle: React.ReactNode;
  title: React.ReactNode;
}

export function CourseMaterialBreadcrumb({
  courseId,
  courseTitle,
  title,
}: Props) {
  const { t } = useTranslation();

  return (
    <Breadcrumb
      className="mb-breadcrumb"
      items={[
        {
          title: <Link to="/">{t('course.courses')}</Link>,
        },
        {
          title: <Link to={`/courses/${courseId}`}>{courseTitle}</Link>,
        },
        {
          title: (
            <Link to={`/courses/edit/${courseId}`}>
              {t('course_edit_page.title')}
            </Link>
          ),
        },
        {
          title,
        },
      ]}
    />
  );
}
