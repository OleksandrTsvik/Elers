import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useCreateCourseMaterialFileMutation } from '../../api/course-materials.mutations.api';
import { FormMode } from '../../common/types';
import {
  CourseTabType,
  MaterialFileForm,
  MaterialFileSubmitValues,
  getCourseEditPagePath,
} from '../../shared';

interface Props {
  courseId: string;
  tabId: string;
  courseTabType: CourseTabType;
}

export default function MaterialFileCreationForm({
  courseId,
  tabId,
  courseTabType,
}: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [createCourseMaterial, { isLoading, error }] =
    useCreateCourseMaterialFileMutation();

  const handleSubmit = async (values: MaterialFileSubmitValues) => {
    await createCourseMaterial({ tabId, ...values })
      .unwrap()
      .then(() =>
        navigate(getCourseEditPagePath(courseId, tabId, courseTabType)),
      );
  };

  return (
    <MaterialFileForm
      mode={FormMode.Creation}
      initialValues={{ title: '', files: [] }}
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
