import { DOWNLOAD_COURSE_MATERIAL_FILE_LINK } from './course-materials.constants';
import useDownloadFile from '../../hooks/use-download-file';

export function useDownloadCourseMaterialFile() {
  const { downloadFileOrOpenPdf } = useDownloadFile();

  const downloadCourseMaterialFile = async (
    uniqueFileName: string,
    fileName: string,
  ) => {
    await downloadFileOrOpenPdf(
      DOWNLOAD_COURSE_MATERIAL_FILE_LINK + uniqueFileName,
      fileName,
    );
  };

  return { downloadCourseMaterialFile };
}
