import { Breadcrumb } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

interface Props {
  courseId: string;
  title: string;
}

export default function MaterialLinkCreationBreadcrumb({
  courseId,
  title,
}: Props) {
  const { t } = useTranslation();

  return (
    <Breadcrumb
      className="mb-breadcrumb"
      items={[
        {
          title: <Link to="/courses">{t('my_courses_page.head_title')}</Link>,
        },
        {
          title: <Link to={`/courses/${courseId}`}>{title}</Link>,
        },
        {
          title: (
            <Link to={`/courses/edit/${courseId}`}>
              {t('course_edit_page.title')}
            </Link>
          ),
        },
        {
          title: t('material_link_creation_page.title'),
        },
      ]}
    />
  );
}
