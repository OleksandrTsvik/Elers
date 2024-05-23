import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useUpdateCourseMaterialLinkMutation } from '../../api/course-materials.api';
import {
  CourseTabType,
  MaterialLinkForm,
  MaterialLinkFormValues,
  getCourseEditPagePath,
} from '../../shared';

interface Props {
  courseId: string;
  courseMaterialId: string;
  tabId: string;
  courseTabType: CourseTabType;
  title: string;
  link: string;
}

export default function MaterialLinkEditForm({
  courseId,
  courseMaterialId,
  tabId,
  courseTabType,
  title,
  link,
}: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [updateCourseMaterial, { isLoading, error }] =
    useUpdateCourseMaterialLinkMutation();

  const handleSubmit = async (values: MaterialLinkFormValues) => {
    await updateCourseMaterial({ id: courseMaterialId, ...values })
      .unwrap()
      .then(() =>
        navigate(getCourseEditPagePath(courseId, tabId, courseTabType)),
      );
  };

  return (
    <MaterialLinkForm
      initialValues={{ title, link }}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
